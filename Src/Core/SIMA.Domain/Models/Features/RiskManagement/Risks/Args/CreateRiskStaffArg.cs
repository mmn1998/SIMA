namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateRiskStaffArg
{
    public long Id { get; set; }
    public long RiskId { get; set; }
    public long StaffId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
