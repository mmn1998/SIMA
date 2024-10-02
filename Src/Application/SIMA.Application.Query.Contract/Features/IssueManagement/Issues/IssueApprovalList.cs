using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues;

public class IssueApprovalList
{
    public long StepApprovalOptionId { get; set; }
    public string? StepApprovalOptionName { get; set; }
    public string? Description { get; set; }
    public string? StepName { get; set; }
    public long StepId { get; set; }
    public long ActorId { get; set; }
    public string? ActorName { get; set; }
    public string? CreatedBy { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedAtPersian => DateHelper.ToPersianDateTime(CreatedAt);
}



