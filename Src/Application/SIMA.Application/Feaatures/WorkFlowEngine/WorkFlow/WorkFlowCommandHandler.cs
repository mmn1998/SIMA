using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.Steps;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Resources;


namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlow
{
    public class WorkFlowCommandHandler : ICommandHandler<CreateWorkFlowCommand, Result<long>>, ICommandHandler<DeleteWorkFlowCommand, Result<long>>,
        ICommandHandler<ModifyWorkFlowCommand, Result<long>>,
        ICommandHandler<CreateStepCommand, Result<long>>, ICommandHandler<Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask.ModifyStepCommand, Result<long>>,
        ICommandHandler<DeleteStepCommand, Result<long>>,
        ICommandHandler<CreateStateCommand, Result<long>>, ICommandHandler<ModifyStateCommand, Result<long>>,
        ICommandHandler<DeleteStateCommand, Result<long>>
    //,ICommandHandler<CreateRejectionReasonCommand, Result<long>>, ICommandHandler<DeleteRejectionReasonCommand, Result<long>>

    {
        private readonly IWorkFlowRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWorkFlowDomainService _service;
        private readonly ISimaIdentity _simaIdentity;

        public WorkFlowCommandHandler(IWorkFlowRepository repository, IUnitOfWork unitOfWork, IMapper mapper,
            IWorkFlowDomainService service, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _simaIdentity = simaIdentity;
        }

        #region Workflow
        public async Task<Result<long>> Handle(CreateWorkFlowCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateWorkFlowArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var entity = await Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities.WorkFlow.New(arg, _service);
            await _repository.Add(entity);
            var id = entity.Id.Value;
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(id);
        }
        public async Task<Result<long>> Handle(ModifyWorkFlowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyWorkFlowArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteWorkFlowCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        #endregion

        #region Step
        public async Task<Result<long>> Handle(CreateStepCommand request, CancellationToken cancellationToken)
        {
            var workflow = await _repository.GetById((long)request.WorkFlowId);
            var arg = _mapper.Map<StepArg>(request);
            arg.UserId = _simaIdentity.UserId;
            var step = workflow.AddStep(arg);


            List<CreateWorkFlowActorStepArg> listActorStepArg = new List<CreateWorkFlowActorStepArg>();
            foreach (var item in request.ActorId)
            {
                CreateWorkFlowActorStepArg actorStepArg = new CreateWorkFlowActorStepArg();
                actorStepArg.WorkFlowActorId = item;
                actorStepArg.StepId = step.Id.Value;
                actorStepArg.ActiveStatusId = (long)ActiveStatusEnum.Active;
                actorStepArg.Id = IdHelper.GenerateUniqueId();
                listActorStepArg.Add(actorStepArg);
            }

            step.AddActorStep(listActorStepArg);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(step.Id.Value);
        }
        public async Task<Result<long>> Handle(ModifyStepCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById((long)request.WorkFlowId);
            var arg = _mapper.Map<ModifyStepArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.ModifyStep(arg);

            if (request.ApprovalOptions is not null && request.ApprovalOptions.Count > 0)
            {
                var approvalArg = _mapper.Map<List<CreateStepApprovalOptionArg>>(request.ApprovalOptions);
                approvalArg.ForEach(it => it.CreatedBy = _simaIdentity.UserId);
                await entity.AddStepApproval(approvalArg, request.Id, _service);
            }
            if (request.RequiredDocuments is not null && request.RequiredDocuments.Count > 0)
            {
                var requirmentDocumentsArg = _mapper.Map<List<CreateStepRequiredDocumentArg>>(request.RequiredDocuments);
                requirmentDocumentsArg.ForEach(it => it.StepId = arg.Id);
                requirmentDocumentsArg.ForEach(it => it.CreatedBy = _simaIdentity.UserId);
                await entity.AddRequirmentDocument(requirmentDocumentsArg, request.Id);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteStepCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.WorkFlowId);
            entity.DeleteStep(request.Id, _simaIdentity.UserId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        #endregion

        #region State
        public async Task<Result<long>> Handle(CreateStateCommand request, CancellationToken cancellationToken)
        {
            if (!await _service.CheckWorkFlow((long)request.WorkFlowId)) throw new SimaResultException(CodeMessges._100057Code, Messages.WorkflowNotFoundError);
            var workflow = await _repository.GetById2(new WorkFlowId(Value: (long)request.WorkFlowId));
            var arg = _mapper.Map<CreateStateArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            var state = await workflow.AddState(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(state.Id.Value);
        }
        public async Task<Result<long>> Handle(ModifyStateCommand request, CancellationToken cancellationToken)
        {
            if (!await _service.CheckWorkFlow((long)request.WorkFlowId)) throw new SimaResultException(CodeMessges._100057Code, Messages.WorkflowNotFoundError);
            var entity = await _repository.GetById((long)request.WorkFlowId);
            var arg = _mapper.Map<ModifyStateArgs>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.ModifyState(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        public async Task<Result<long>> Handle(DeleteStateCommand request, CancellationToken cancellationToken)
        {
            if (!await _service.CheckWorkFlow(request.WorkFlowId)) throw new SimaResultException(CodeMessges._100057Code, Messages.WorkflowNotFoundError);
            var entity = await _repository.GetById(request.WorkFlowId);
            entity.DeleteState(request.Id, _simaIdentity.UserId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        #endregion
    }
}
