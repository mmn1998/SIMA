namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateRiskImpactForRiskCommand
{
    public long ImpactScaleId { get; set; }
    public long RiskImpactId { get; set; }
}
