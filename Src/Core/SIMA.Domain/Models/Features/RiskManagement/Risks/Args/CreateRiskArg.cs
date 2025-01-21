namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateRiskArg
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public long RiskTypeId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
