using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;

namespace SIMA.Application.Feaatures.IssueManagement.Issues
{
    public class IssueCommandHandler : ICommandHandler<CreateIssueCommand, Result<long>>, ICommandHandler<ModifyIssueCommand, Result<long>>
    , ICommandHandler<DeleteIssueCommand, Result<long>>, ICommandHandler<DeleteIssueCommentCommand, Result<long>>,
      ICommandHandler<CreateIssueCommentCommand, Result<long>>
     , ICommandHandler<IssueRunActionCommand, Result<long>>
    {
        private readonly IIssueRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IIssueDomainService _service;
        private readonly IWorkFlowRepository _workFlowRepository;
        private readonly IIssueWeightCategoryQueryRepository _issueWeightCategoryRepository;
        private readonly IProjectRepository _projectRepository;

        public IssueCommandHandler(IIssueRepository repository, IUnitOfWork unitOfWork,
            IMapper mapper, IIssueDomainService service,
          IWorkFlowRepository workFlowRepository, IIssueWeightCategoryQueryRepository issueWeightCategoryRepository, IProjectRepository projectRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _service = service;
            _workFlowRepository = workFlowRepository;
            _issueWeightCategoryRepository = issueWeightCategoryRepository;
            _projectRepository = projectRepository;
        }
        public async Task<Result<long>> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var arg = _mapper.Map<CreateIssueArg>(request);
            arg.IssueWeightCategoryd = await _issueWeightCategoryRepository.GetIdByWeight((int)request.Weight);

            var workflow = await _workFlowRepository.GetWorkflowInfoById(request.CurrentWorkflowId);
            arg.CurrenStepId = workflow.TargetStepId;
            arg.CurrentStateId = workflow.TargetStateId;

            #region GenerateCode

            var project = await _projectRepository.GetById(workflow.ProjectId);
            var lastIsuue = await _repository.GetLastIssue();
            var code = Convert.ToInt32(lastIsuue.Code.Substring(lastIsuue.Code.IndexOf("-") + 1)) + 1;
            arg.Code = project.Code + "-" + code.ToString();

            #endregion


            var entity = await Issue.Create(arg, _service);
            if (arg.IssueLinks is not null || arg.IssueLinks.Count > 0)
                entity.AddIssueLink(arg.IssueLinks);
            if (arg.IssueDocument is not null && arg.IssueDocument.Count > 0)
                entity.AddIssueDocument(arg.IssueDocument);

            #region IssueHistory

            var history = _mapper.Map<CreateIssueHistoryArg>(request);
            history.SourceStateId = workflow.SourceStateId;
            history.TargetStateId = workflow.TargetStateId;
            history.SourceStepId = workflow.SourceStepId;
            history.TargetStepId = workflow.TargetStepId;
            entity.AddHistory(history);

            #endregion



            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }



        public async Task<Result<long>> Handle(ModifyIssueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyIssueArg>(request);
            entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(IssueRunActionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.IssueId);
            var nextStep = await _workFlowRepository.GetNextStepById(entity.CurrentWorkflowId, request.NextStepId);

            var arg = _mapper.Map<IssueRunActionArg>(request);
            arg.CurrentStepId = nextStep.SourceStepId;
            arg.CurrentStateId = nextStep.SourceStateId;

            if (!string.IsNullOrEmpty(request.Comment))
            {
                var commentArg = _mapper.Map<CreateIssueCommentArg>(request);
                await entity.AddComment(commentArg);
            }

            #region IssueHistory

            var history = _mapper.Map<CreateIssueHistoryArg>(entity);
            history.SourceStateId = entity.CurrentStateId;
            history.TargetStateId = nextStep.SourceStateId;
            history.SourceStepId = entity.CurrenStepId;
            history.TargetStepId = nextStep.SourceStepId;
            entity.AddHistory(history);

            #endregion

            entity.RunAction(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.IssueId);
        }

        public async Task<Result<long>> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            entity.Deactive();
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }
        public async Task<Result<long>> Handle(CreateIssueCommentCommand request, CancellationToken cancellationToken)
        {
            var issue = await _repository.GetById(request.IssueId);
            var arg = _mapper.Map<CreateIssueCommentArg>(request);
            await issue.AddComment(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.IssueId);
        }

        public async Task<Result<long>> Handle(DeleteIssueCommentCommand request, CancellationToken cancellationToken)
        {
            var issue = await _repository.GetById(request.IssueId);
            issue.DeactiveComment(new(request.Id));
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.IssueId);
        }
    }
}
