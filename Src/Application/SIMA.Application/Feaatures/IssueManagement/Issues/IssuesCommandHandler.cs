using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Exceptions;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;
using SIMA.Resources;

namespace SIMA.Application.Feaatures.IssueManagement.Issues;

public class IssueCommandHandler : ICommandHandler<CreateIssueCommand, Result<long>>, ICommandHandler<ModifyIssueCommand, Result<long>>
, ICommandHandler<DeleteIssueCommand, Result<long>>, ICommandHandler<DeleteIssueCommentCommand, Result<long>>,
  ICommandHandler<CreateIssueCommentCommand, Result<long>>
 , ICommandHandler<IssueRunActionCommand, Result<long>>
{

    private readonly IIssueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIssueDomainService _service;
    private readonly IWorkFlowDomainService _workFlowDomainService;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IWorkFlowRepository _workFlowRepository;
    private readonly IIssueWeightCategoryQueryRepository _issueWeightCategoryRepository;
    private readonly IProjectRepository _projectRepository;

    public IssueCommandHandler(IIssueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IIssueDomainService service,
      IWorkFlowRepository workFlowRepository, IIssueWeightCategoryQueryRepository issueWeightCategoryRepository,
      IProjectRepository projectRepository, IWorkFlowDomainService workFlowDomainService, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _service = service;
        _workFlowRepository = workFlowRepository;
        _issueWeightCategoryRepository = issueWeightCategoryRepository;
        _projectRepository = projectRepository;
        _workFlowDomainService = workFlowDomainService;
        _simaIdentity = simaIdentity;
    }
    public async Task<Result<long>> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!await _workFlowDomainService.CheckCreateIssueWithActor(request.CurrentWorkflowId)) throw new SimaResultException(CodeMessges._400Code, Messages.CreateIssueWithChechActorException);

            var workflow = await _workFlowRepository.GetWorkflowInfoById(request.CurrentWorkflowId);
            request.MainAggregateId = workflow.MainAggregateId;
            var arg = _mapper.Map<CreateIssueArg>(request);
            arg.CreatedBy = _simaIdentity.UserId;
            arg.IssueWeightCategoryd = await _issueWeightCategoryRepository.GetIdByWeight((int)request.Weight);
            arg.CurrenStepId = workflow.TargetStepId;
            arg.CurrentStateId = workflow.TargetStateId;
            arg.CreatedBy = _simaIdentity.UserId;

            #region GenerateCode

            var project = await _projectRepository.GetById(workflow.ProjectId);
            var lastIsuue = await _repository.GetLastIssue();
            string codde = "";
            if (lastIsuue == null)
            {
                codde = "356";
            }
            else
            {
                codde = lastIsuue.Code;
            }
            var code = Convert.ToInt32(codde.Substring(codde.IndexOf("-") + 1)) + 1;
            arg.Code = project.Code + "-" + code.ToString();

            #endregion

            var historyArg = _mapper.Map<CreateIssueChangeHistoryArg>(arg);
            historyArg.CreatedBy = _simaIdentity.UserId;
            var entity = await Issue.Create(arg, _service);
            entity.AddIssueLink(arg.IssueLinks);
            entity.AddIssueDocument(arg.IssueDocument);

            #region IssueHistory
            var history = _mapper.Map<CreateIssueHistoryArg>(request);
            history.CreatedBy = _simaIdentity.UserId;
            history.SourceStateId = workflow.SourceStateId;
            history.TargetStateId = workflow.TargetStateId;
            history.SourceStepId = workflow.SourceStepId;
            history.TargetStepId = workflow.TargetStepId;
            entity.AddHistory(history);
            #endregion
            entity.AddIssueChangeHistory(historyArg);
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<Result<long>> Handle(ModifyIssueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyIssueArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        arg.IssueWeightCategoryd = await _issueWeightCategoryRepository.GetIdByWeight((int)request.Weight);
        var historyArg = _mapper.Map<CreateIssueChangeHistoryArg>(arg);
        historyArg.CreatedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);
        historyArg.CompanyId = entity.CompanyId;
        historyArg.MainAggregateId = entity.MainAggregateId.Value;
        historyArg.SourceId = entity.SourceId;
        historyArg.CurrenStepId = entity.CurrenStepId.Value;
        if (entity.CurrentStateId is not null)
            historyArg.CurrentStateId = entity.CurrentStateId.Value;
        historyArg.Code = entity.Code;
        entity.AddIssueChangeHistory(historyArg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }

    public async Task<Result<long>> Handle(IssueRunActionCommand request, CancellationToken cancellationToken)
    {
        ///
        ///  TODO : new changes in run action
        ///
        //if ((string.Equals(request.RunActionType, RunActionType.Sp.ToString(), StringComparison.InvariantCultureIgnoreCase) ||
        //    string.Equals(request.RunActionType, RunActionType.Both.ToString(), StringComparison.InvariantCultureIgnoreCase))
        //    && request.SpName is not null)
        //{
        //    await _repository.ExcecuteStoreProcedure(request.SpName);
        //}

        var issue = await _repository.GetById(request.IssueId);
        var nextStep = await _workFlowRepository.GetNextStepById(issue.CurrentWorkflowId.Value, request.NextStepId, request.ProgressId);

        var arg = _mapper.Map<IssueRunActionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        arg.CurrentStepId = nextStep.SourceStepId;
        arg.CurrentStateId = nextStep.SourceStateId;

        if (!string.IsNullOrEmpty(request.Comment))
        {
            var commentArg = _mapper.Map<CreateIssueCommentArg>(request);
            await issue.AddComment(commentArg);
        }

        #region IssueHistory
        var history = _mapper.Map<CreateIssueHistoryArg>(issue);
        history.CreatedBy = _simaIdentity.UserId;
        if (issue.CurrentStateId is not null)
            history.SourceStateId = issue.CurrentStateId.Value;
        history.TargetStateId = nextStep.SourceStateId;
        history.SourceStepId = issue.CurrenStepId.Value;
        history.TargetStepId = nextStep.SourceStepId;
        issue.AddHistory(history);

        #endregion

        issue.RunAction(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.IssueId);
    }

    public async Task<Result<long>> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.Id);
    }
    public async Task<Result<long>> Handle(CreateIssueCommentCommand request, CancellationToken cancellationToken)
    {
        var issue = await _repository.GetById(request.IssueId);
        var arg = _mapper.Map<CreateIssueCommentArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        await issue.AddComment(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.IssueId);
    }

    public async Task<Result<long>> Handle(DeleteIssueCommentCommand request, CancellationToken cancellationToken)
    {
        var issue = await _repository.GetById(request.IssueId);
        issue.DeleteComment(new(request.Id));
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.IssueId);
    }
}
