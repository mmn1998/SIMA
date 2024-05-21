namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisAnnouncementArg
{
    public long StaffId { get; set; }
    public long BusinessImpactAnalysisId { get; set; }
    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
