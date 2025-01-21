namespace SIMA.Application.Contract.Features.RiskManagers.Risks;

public class CreateThreatCommand
{
    public long ThreatTypeId { get; set; }
    public long RiskPossibilityId { get; set; }
    public string? Description { get; set; }
}
