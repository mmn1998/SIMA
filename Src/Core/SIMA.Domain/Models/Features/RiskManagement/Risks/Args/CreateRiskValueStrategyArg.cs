namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Args;

public class CreateRiskValueStrategyArg
{
    public long Id { get; set; }
    public long RiskId { get; set; }
    public long StrategyId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}