using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Interface;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.Issues;
using SIMA.Persistance.Read.Repositories.Features.IssueManagement.IssueWeightCategories;
using SIMA.Persistance.Read.Repositories.Features.WorkFlowEngine.WorkFlow;
using SIMA.Resources;

namespace SIMA.Application.Feaatures.IssueManagement.Issues;

public class IssueCommandHandler : ICommandHandler<CreateIssueCommand, Result<long>>, ICommandHandler<ModifyIssueCommand, Result<long>>
, ICommandHandler<DeleteIssueCommand, Result<long>>, ICommandHandler<DeleteIssueCommentCommand, Result<long>>,
  ICommandHandler<CreateIssueCommentCommand, Result<long>>
 , ICommandHandler<IssueRunActionCommand, Result<long>>,
    ICommandHandler<ModifyIssueAssigneeCommand, Result<long>>
{

    private readonly IIssueRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IIssueDomainService _service;
    private readonly IWorkFlowDomainService _workFlowDomainService;
    private readonly ISimaIdentity _simaIdentity;
    private readonly IWorkFlowRepository _workFlowRepository;
    private readonly IWorkFlowActorRepository _workFlowActorRepository;
    private readonly IIssueQueryRepository _issueQueryRepository;
    private readonly IWorkFlowQueryRepository _workFlowQueryRepository;
    private readonly IIssueWeightCategoryQueryRepository _issueWeightCategoryRepository;
    private readonly IProjectRepository _projectRepository;

    public IssueCommandHandler(IIssueRepository repository, IUnitOfWork unitOfWork,
        IMapper mapper, IIssueDomainService service,
      IWorkFlowRepository workFlowRepository, IIssueWeightCategoryQueryRepository issueWeightCategoryRepository,
      IProjectRepository projectRepository, IWorkFlowDomainService workFlowDomainService, ISimaIdentity simaIdentity, IWorkFlowQueryRepository workFlowQueryRepository, IWorkFlowActorRepository workFlowActorRepository, IIssueQueryRepository issueQueryRepository)
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
        _workFlowQueryRepository = workFlowQueryRepository;
        _workFlowActorRepository = workFlowActorRepository;
        _issueQueryRepository = issueQueryRepository;
    }
    public async Task<Result<long>> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (!await _workFlowDomainService.CheckCreateIssueWithActor(request.CurrentWorkflowId))
                throw new SimaResultException(CodeMessges._400Code, Messages.CreateIssueWithChechActorException);

            var workflow = await _workFlowQueryRepository.GetWorkflowInfoByIdAsync(request.CurrentWorkflowId);
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
            foreach (var issueLink in arg.IssueLinks)
            {
                issueLink.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddIssueLink(arg.IssueLinks);
            foreach (var issueDocument in arg.IssueDocument)
            {
                issueDocument.CreatedBy = _simaIdentity.UserId;
            }
            await entity.AddIssueDocument(arg.IssueDocument);

            #region UpdateDocuments

            #endregion

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

        var userId = _simaIdentity.UserId;

        var issue = await _repository.GetById(request.IssueId);

        var documents = PrepareDocuments(request.InputDocuments, issue.SourceId, userId);

        var nextStepModel = PrepareSystemParams(request, issue.SourceId);

        var inputParams = MapInputParams(request.InputParams);

        var mainAggregateName = Enum.GetName(typeof(MainAggregateEnums), issue.MainAggregateId.Value);

        await _workFlowQueryRepository.ExecuteSP(request.ProgressId, mainAggregateName, nextStepModel.SystemParams, inputParams, documents);

        var nextStep = await _workFlowQueryRepository.GetNextStepById(issue.CurrentWorkflowId.Value, nextStepModel);

        AddHistory(userId, issue, nextStep.SourceStateId, nextStep.SourceStepId);

        #region Comment

        if (!string.IsNullOrEmpty(request.Comment))
        {
            var commentArg = _mapper.Map<CreateIssueCommentArg>(request);
            await issue.AddComment(commentArg);
        }

        #endregion

        #region Approval

        if (request.StepApprovalOptionId is not null && request.StepApprovalOptionId > 0)
        {
            var addApproval = await AddApprovalOption(userId, request, issue.CurrentWorkflowId.Value);
            await issue.AddIssueApproval(addApproval);
        }

        #endregion

        var arg = _mapper.Map<IssueRunActionArg>(request);
        arg.ModifiedBy = userId;
        arg.CurrentStepId = nextStep.SourceStepId;
        arg.CurrentStateId = nextStep.SourceStateId;

        issue.RunAction(arg);

        if (nextStep.ActionTypeId == (long)ActionTypeEnum.endEvent)
            issue.Finish(_simaIdentity.UserId);

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.IssueId);
    }
    public async Task<Result<long>> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
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
        issue.DeleteComment(new(request.Id), _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.IssueId);
    }

    #region privateMethod

    private List<AddDocumentToSPQuery> PrepareDocuments(IEnumerable<InputDocument> inputDocuments, long sourceId, long userId)
    {
        var documents = _mapper.Map<List<AddDocumentToSPQuery>>(inputDocuments);
        foreach (var document in documents)
        {
            document.SourceId = sourceId.ToString();
            document.CreatedBy = userId.ToString();
        }
        return documents;
    }
    private GetNextStepQuery PrepareSystemParams(IssueRunActionCommand request, long sourceId)
    {
        var nextStepModel = _mapper.Map<GetNextStepQuery>(request);
        nextStepModel.SystemParams.Add(new InputModel { Key = "IssueId", Value = request.IssueId.ToString() });
        nextStepModel.SystemParams.Add(new InputModel { Key = "UserId", Value = _simaIdentity.UserId.ToString() });
        nextStepModel.SystemParams.Add(new InputModel { Key = "CreatedBy", Value = _simaIdentity.UserId.ToString() });
        nextStepModel.SystemParams.Add(new InputModel { Key = "CompanyId", Value = _simaIdentity.CompanyId.ToString() });
        nextStepModel.SystemParams.Add(new InputModel { Key = "SourceId", Value = sourceId.ToString() });
        return nextStepModel;
    }
    private List<InputParamQueryModel>? MapInputParams(List<InputParamModel> inputParams)
    {
        var paramList = inputParams?.Select(param => new InputParamQueryModel
        {
            Id = param.Id,
            ParamName = param.ParamName,
            ParamValue = param.ParamValue
        }).ToList();

        return paramList;
    }
    private void AddHistory(long userId, Issue issue, long? sourceStateId, long? sourceStepId)
    {
        var history = _mapper.Map<CreateIssueHistoryArg>(issue);
        history.CreatedBy = userId;
        if (issue.CurrentStateId is not null)
            history.SourceStateId = issue.CurrentStateId.Value;
        history.TargetStateId = sourceStateId;
        history.SourceStepId = issue.CurrenStepId.Value;
        history.TargetStepId = sourceStepId;
        history.PerformerUserId = _simaIdentity.UserId;
        issue.AddHistory(history);
    }
    private async Task<CreateIssueApprovalArg> AddApprovalOption(long userId, IssueRunActionCommand request, long workflowId)
    {
        var approvalArg = _mapper.Map<CreateIssueApprovalArg>(request);
        approvalArg.ApprovedBy = userId;
        approvalArg.CreatedBy = userId;
        var workFlowActor = await _workFlowActorRepository.GetWorkFlowActorByUser(workflowId);
        approvalArg.WorkflowActorId = workFlowActor.Id.Value;
        return approvalArg;
    }

    public async Task<Result<long>> Handle(ModifyIssueAssigneeCommand request, CancellationToken cancellationToken)
    {
        var issue = await _repository.GetById(request.IssueId);
        issue.AddAsiggnee(request.UserId);
        return Result.Ok(request.IssueId);
    }

    #endregion


}
