namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;

public class ModifyBusinessContinuityPlanArg
{
    public long Id { get; set; }
    public string Scope { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string VersionNumber { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
