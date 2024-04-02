using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueChangeHistory : Entity
{
    private IssueChangeHistory() { }
    private IssueChangeHistory(CreateIssueChangeHistoryArg arg)
    {
        Id = new IssueChangeHistoryId(arg.Id);
        Code = arg.Code;
        Summery = arg.Summery;
        CurrentWorkflowId = new(arg.CurrentWorkflowId);
        CurrentStateId = new(arg.CurrentStateId);
        CurrenStepId = new(arg.CurrenStepId);
        MainAggregateId = new(arg.MainAggregateId);
        SourceId = arg.SourceId;
        IssueTypeId = new(arg.IssueTypeId);
        IssuePriorityId = new(arg.IssuePriorityId);
        IssueWeightCategoryId = new(arg.IssueWeightCategoryd);
        Weight = arg.Weight;
        IssueDate = arg.IssueDate;
        Description = arg.Description;
        DueDate = arg.DueDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        CompanyId = arg.CompanyId;
    }

    public static  IssueChangeHistory Create(CreateIssueChangeHistoryArg arg)
    {
        return new IssueChangeHistory(arg);
    }

    public IssueChangeHistoryId Id { get; private set; }
    public long CompanyId { get; private set; }
    public WorkFlowId CurrentWorkflowId { get; private set; }
    public virtual WorkFlow CurrentWorkflow { get; private set; }
    public string Code { get; private set; }
    public string Summery { get; private set; }
    public StateId CurrentStateId { get; private set; }
    public IssueId IssueId { get; private set; }
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
    public IssueWeightCategoryId IssueWeightCategoryId { get; private set; }
    public virtual IssueWeightCategory IssueWeightCategory { get; private set; }
    public virtual Issue Issue { get; private set; }
    public int Weight { get; private set; }
    public DateTime IssueDate { get; private set; }
    public string? Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
}


