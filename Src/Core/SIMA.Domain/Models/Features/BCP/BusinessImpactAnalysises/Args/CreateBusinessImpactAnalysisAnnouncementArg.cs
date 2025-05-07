namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class CreateBusinessImpactAnalysisAnnouncementArg
{
    public long StaffId { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}