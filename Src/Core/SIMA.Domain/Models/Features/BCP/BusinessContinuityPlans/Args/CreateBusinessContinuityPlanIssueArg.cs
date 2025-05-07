namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class CreateBusinessContinuityPlanIssueArg
{
    public long Id { get; set; }
    public long IssueId { get; set; }
    public long BusinessContinuityPlanId { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public long ActiveStatusId { get; set; }
}
