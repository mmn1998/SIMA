using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class CreateBusinessContinuityPlanArg
{
    public long Id { get; set; }
    public string Scope { get; set; }
    public string Code { get; set; } = IdHelper.GenerateUniqueId().ToString();
    public string Title { get; set; }
    public string VersionNumber { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public long? CreatedBy { get; set; }
    public long IssueId { get; set; }
}