namespace SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Args;

public class CreateServiceRiskImpactArg
{
    public long Id { get; set; }
    public long ServiceRiskId { get; set; }
    public long ImpactScaleId { get; set; }
    public long RiskImpactId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
