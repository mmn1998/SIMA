using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;
using SIMA.Application.Feaatures.WorkFlowEngine.BPMSes.Mappers;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.WorkFlowEngine.BPMSes;

public class BpmsCommandHandler : ICommandHandler<CreateBpmsCommand, Result<long>>, ICommandHandler<ModifyBpmsCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkFlowRepository _workflowRepository;
    private readonly IProgressRepository _progressRepository;
    private readonly IWorkFlowActorRepository _actorRepository;
    private readonly ISimaIdentity _simaIdentity;

    public BpmsCommandHandler(IMapper mapper,
        IUnitOfWork unitOfWork,
        IWorkFlowRepository workflowRepository,
        IProgressRepository progressRepository,
        IWorkFlowActorRepository actorRepository, ISimaIdentity simaIdentity)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _workflowRepository = workflowRepository;
        _progressRepository = progressRepository;
        _actorRepository = actorRepository;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateBpmsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var workflow = await _workflowRepository.GetById(request.WorkFlowId);
            var arg = BpmsMapper.Map(request);
            arg.ModifyBy = _simaIdentity.UserId;
            //foreach (var step in arg.Steps) step.UserId = _simaIdentity.UserId;
            //foreach (var item in arg.Progresses) item.CreatedBy = _simaIdentity.UserId;
            //foreach (var item in arg.WorkFlowActors) item.UserId = _simaIdentity.UserId;
            workflow.Modify(arg);
            await _unitOfWork.SaveChangesAsync();
            var workFlowId = workflow.Id.Value;
            return Result.Ok(workFlowId);
        }
        catch (Exception ex)
        {

            throw;
        }


    }

    public async Task<Result<long>> Handle(ModifyBpmsCommand request, CancellationToken cancellationToken)
    {
        var workflow = await _workflowRepository.GetById(request.WorkFlowId);

        var arg = BpmsMapper.Map(request, workflow);
        arg.ModifyBy = _simaIdentity.UserId;
        workflow.ModifyFlow(arg);
        await _unitOfWork.SaveChangesAsync();
        var workFlowId = workflow.Id.Value;
        return Result.Ok(workFlowId);
    }
}
