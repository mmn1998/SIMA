namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateserviceRiskImpactCommand
{
    public long ServiceId { get; set; }
    public List<CreateRiskImpactForRiskCommand>? RiskImpactList { get; set; }
}
