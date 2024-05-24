namespace SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;

public class ModifyBusinessImpactAnalysisStaffArg
{
    public long BusinessImpactAnalysisId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
