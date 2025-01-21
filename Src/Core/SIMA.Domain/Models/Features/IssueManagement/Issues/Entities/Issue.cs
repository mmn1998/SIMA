using SIMA.Domain.Models.Features.AccessManagement.AccessRequests.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Interfaces;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class Issue : Entity
{

    private Issue() { }
    private Issue(CreateIssueArg arg)
    {
        Id = new IssueId(arg.Id);
        Code = arg.Code;
        Summery = arg.Summery;
        CurrentWorkflowId = new(arg.CurrentWorkflowId);
        CurrentStateId = arg.CurrentStateId != 0 ? new(arg.CurrentStateId) : null;
        CurrenStepId = new(arg.CurrenStepId);
        MainAggregateId = new(arg.MainAggregateId);
        SourceId = arg.SourceId;
        IssueTypeId = new(arg.IssueTypeId);
        IssuePriorityId = new(arg.IssuePriorityId);
        IssueWeightCategoryId = arg.IssueWeightCategoryd != 0 ? new(arg.IssueWeightCategoryd) : null;
        Weight = arg.Weight;
        IssueDate = arg.IssueDate;
        Description = arg.Description;
        DueDate = arg.DueDate;
        CompanyId = arg.CompanyId;
        RequesterId = arg.RequesterId != 0 ? new UserId(arg.RequesterId) : null;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;

    }

    public static async Task<Issue> Create(CreateIssueArg arg, IIssueDomainService service)
    {

        await GuardAgainstCodeUniqueness(arg.Id, arg.Code, service);
        await CreateGuards(arg, service);

        return new Issue(arg);
    }

    private static async Task GuardAgainstCodeUniqueness(long id, string code, IIssueDomainService service)
    {
        var isCodeUnique = await service.IsCodeUnique(code, id);
        if (isCodeUnique)
        {
            throw new SimaResultException(CodeMessges._400Code, Messages.IssueCodeIsNotUnique);
        }
    }

    public void AddHistory(CreateIssueHistoryArg arg)
    {
        _issueHistories.Add(IssueHistory.Create(arg));
    }

    public async Task Modify(ModifyIssueArg arg, IIssueDomainService service)
    {
        await ModifyGuards(arg, service);
        IssueTypeId = new(arg.IssueTypeId);
        IssuePriorityId = new(arg.IssuePriorityId);
        IssueWeightCategoryId = arg.IssueWeightCategoryd.HasValue ? new(arg.IssueWeightCategoryd.Value) : null;
        Weight = arg.Weight;
        Description = arg.Description;
        Summery = arg.Summery;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        DueDate = arg.DueDate;
    }
    public void AddIssueChangeHistory(CreateIssueChangeHistoryArg arg)
    {
        IssueChangeHistory.Create(arg);
    }

    public void AddIssueManagers(List<CreateIssueManagerArg> args)
    {
        foreach (CreateIssueManagerArg arg in args)
        {
            arg.IssueId = Id.Value;
            var manager = IssueManager.Create(arg);
            _issueManager.Add(manager);
        }
    }

    public void RunAction(IssueRunActionArg arg)
    {
        CurrentStateId = arg.CurrentStateId == null ? null : new(arg.CurrentStateId.Value); //new((long)arg.CurrentStateId);
        CurrenStepId = new(arg.CurrentStepId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        AssigneeId = null;
    }
    public async Task AddComment(CreateIssueCommentArg issueCommentArg)
    {
        var comment = await IssueComment.Create(issueCommentArg);
        _issueComments.Add(comment);
    }

    public async Task AddIssueApproval(CreateIssueApprovalArg issueApproval)
    {
        var approval = await IssueApproval.Create(issueApproval);
        _issueApprovals.Add(approval);
    }
    public void DeleteComment(IssueCommentId issueCommentId, long userId)
    {
        var comment = _issueComments.FirstOrDefault(c => c.Id == issueCommentId);
        comment.NullCheck();
        comment?.Delete(userId);
    }
    public bool DeleteIssueLink(long issueLinkId, long userId)
    {
        var result = _issueLink.Where(x => x.Id == new IssueLinkId(issueLinkId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(userId);
            return true;
        }
        else
            return false;

    }
    public bool DeleteIssueDocument(long issueDocumentId, long userId)
    {
        var result = _issueDocuments.Where(x => x.Id == new IssueDocumentId(issueDocumentId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(userId);
            return true;
        }
        else
            return false;

    }


    public async Task AddIssueLink(List<CreateIssueLinkArg> issueLinkArgs)
    {
        if (!issueLinkArgs.Any())
            return;
        foreach (var item in issueLinkArgs)
        {
            if (item.IssueIdLinkedTo > 0)
            {
                item.IssueId = Id.Value;
                var issuelink = await IssueLink.Create(item);
                _issueLink.Add(issuelink);
            }

        }
    }
    public async Task AddIssueDocument(List<CreateIssueDocumentArg> issueDocumentArgs)
    {
        if (!issueDocumentArgs.Any())
            return;
        foreach (var item in issueDocumentArgs)
        {
            if (item.DocumentId > 0)
            {
                var issueDocument = await IssueDocument.Create(item);
                _issueDocuments.Add(issueDocument);
            }

        }
    }


    public IssueId Id { get; private set; }
    public long CompanyId { get; private set; }
    public WorkFlowId CurrentWorkflowId { get; private set; }
    public virtual WorkFlow CurrentWorkflow { get; private set; }
    public string Code { get; private set; }
    public string Summery { get; private set; }
    public StateId? CurrentStateId { get; private set; }
    public virtual State CurrentState { get; private set; }
    public StepId CurrenStepId { get; private set; }
    public virtual Step CurrenStep { get; private set; }
    public MainAggregateId? MainAggregateId { get; private set; }
    public virtual MainAggregate? MainAggregate { get; private set; }
    public long SourceId { get; private set; }
    public IssueTypeId IssueTypeId { get; private set; }
    public virtual IssueType IssueType { get; private set; }
    public IssuePriorityId IssuePriorityId { get; private set; }
    public virtual IssuePriority IssuePriority { get; private set; }
    public IssueWeightCategoryId? IssueWeightCategoryId { get; private set; }
    public virtual IssueWeightCategory IssueWeightCategory { get; private set; }
    public int? Weight { get; private set; }
    public DateTime IssueDate { get; private set; }
    public string? Description { get; private set; }
    public DateTime? DueDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public UserId? AssigneeId { get; private set; }
    public UserId? RequesterId { get; private set; }
    public virtual User Requester { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public string? IsFinished { get; private set; }
    public DateTime? FinishedDate { get; private set; }
    public long? FinishedBy { get; private set; }


    public void Finish(long userId)
    {
        FinishedDate = DateTime.Now;
        IsFinished = "1";
        FinishedBy = userId;
    }

    public void AddAsiggnee(long assigneeId)
    {
        AssigneeId = new UserId(assigneeId);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeleteRealatedEntities
        foreach (var meeting in _meetings)
        {
            meeting.Delete(userId);
        }
        #endregion
    }
    public void Activate(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        #region ActivateRealatedEntities
        foreach (var meeting in _meetings)
        {
            meeting.Activate(userId);
        }
        #endregion
    }
    private List<IssueComment> _issueComments = new();
    public ICollection<IssueComment> IssueComments => _issueComments;

    private List<IssueDocument> _issueDocuments = new();
    public ICollection<IssueDocument> IssueDocuments => _issueDocuments;

    private List<IssueLink> _issueLink = new();
    public ICollection<IssueLink> IssueLinks => _issueLink;
    private List<IssueHistory> _issueHistories = new();
    public ICollection<IssueHistory> IssueHistories => _issueHistories;
    public ICollection<IssueLink> IssuesLinkedTo { get; private set; }
    private List<IssueCustomFeild> _issueCustomFeilds = new();
    public ICollection<IssueCustomFeild> IssueCustomFeilds => _issueCustomFeilds;

    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
    private List<Approval> _approvals = new();
    public ICollection<Approval> Approvals => _approvals;
    private List<Meeting> _meetings = new();
    public ICollection<Meeting> Meetings => _meetings;

    private List<ServiceRelatedIssue> _serviceRelatedIssues = new();
    public ICollection<ServiceRelatedIssue> ServiceRelatedIssues => _serviceRelatedIssues;

    private List<RiskRelatedIssue> _riskRelatedIssues = new();
    public ICollection<RiskRelatedIssue> RiskRelatedIssues => _riskRelatedIssues;
    private List<LogisticsRequest> _logistics = new();
    public ICollection<LogisticsRequest> Logistics => _logistics;

    private List<IssueApproval> _issueApprovals = new();
    public ICollection<IssueApproval> IssueApprovals => _issueApprovals;
    private List<AssetIssue> _assetIssues = new();
    public ICollection<AssetIssue> AssetIssues => _assetIssues;
    private List<ConfigurationItemIssue> _configurationItemIssues = new();
    public ICollection<ConfigurationItemIssue> ConfigurationItemIssues => _configurationItemIssues;

    private List<IssueManager> _issueManager = new();
    public ICollection<IssueManager> IssueManagers => _issueManager;
    private List<CriticalActivity> _criticalActivities = new();
    public ICollection<CriticalActivity> CriticalActivities => _criticalActivities;

    private List<LogisticsSupply> _logisticsSupplies = new();
    public ICollection<LogisticsSupply> LogisticsSupplies => _logisticsSupplies;

    private List<AccessRequest> _accessRequests = new();
    public ICollection<AccessRequest> AccessRequests => _accessRequests;

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<BusinessContinuityPlanIssue> _businessContinuityPlanIssues = new();
    public ICollection<BusinessContinuityPlanIssue> BusinessContinuityPlanIssues => _businessContinuityPlanIssues;
    private List<BusinessImpactAnalysisIssue> _businessImpactAnalysisIssues = new();
    public ICollection<BusinessImpactAnalysisIssue> BusinessImpactAnalysisIssues => _businessImpactAnalysisIssues;
    private List<BusinessContinuityStrategyIssue> _businessContinuityStrategyIssues = new();
    public ICollection<BusinessContinuityStrategyIssue> BusinessContinuityStrategyIssues => _businessContinuityStrategyIssues;

    #region Gaurds

    private static async Task CreateGuards(CreateIssueArg arg, IIssueDomainService service)
    {
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (arg.DueDate != null)
        {
            var checkDuDate = await service.CheckDueDate(arg.DueDate.Value);
            if (!checkDuDate) throw new SimaResultException(CodeMessges._400Code, Messages.DueDateError);
        }

    }
    private async Task ModifyGuards(ModifyIssueArg arg, IIssueDomainService service)
    {
        arg.ActiveStatusId.NullCheck();
    }
    #endregion
}


