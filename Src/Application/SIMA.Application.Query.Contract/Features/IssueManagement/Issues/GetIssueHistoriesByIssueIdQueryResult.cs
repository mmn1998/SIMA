using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class GetIssueHistoriesByIssueIdQueryResult
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long? IssueId { get; set; }
    public long SourceStateId { get; set; }
    public long TargetStateId { get; set; }
    public long SourceStepId { get; set; }
    public long TargetStepId { get; set; }
    public long PerformerUserId { get; set; }
    public string? Description { get; set; }
    public string? CurrentStepName { get; set; }
    public string? CurrentStateName { get; set; }
    public string? TargetStepName { get; set; }
    public string? TargetStateName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string PerformerFirstName { get; set; }
    public string PerformerLastName { get; set; }
    public string CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);

}
