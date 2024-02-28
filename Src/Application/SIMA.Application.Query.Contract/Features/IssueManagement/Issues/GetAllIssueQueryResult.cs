using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetAllIssueQueryResult
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long WorkflowId { get; set; }
    public string WorkFlowName { get; set; }
    public long MainAggregateId { get; set; }
    public long SourceId { get; set; }
    public long IssueTypeId { get; set; }
    public string IssueTypeName { get; set; }
    public long IssuePriorityId { get; set; }
    public string IssuePriorityName { get; set; }
    public int Weight { get; set; }
    public long CurrentStateId { get; set; }
    public string CurrentStateName { get; set; }
    public long CurrentStepId { get; set; }
    public string CurrentStepName { get; set; }
    public string Summery { get; set; }
    public string Description { get; set; }
    public string? ActiveStatus { get; set; }
    public long ActiveStatusId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CreatedBy { get; set; }
    public string CreateFullName => $"{FirstName} {LastName}";
    public DateTime CreatedAt { get; set; }
    public string PersianCreatedAt => DateHelper.ToPersianDate(CreatedAt);
}
