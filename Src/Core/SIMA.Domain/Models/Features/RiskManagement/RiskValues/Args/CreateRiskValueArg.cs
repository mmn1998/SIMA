namespace SIMA.Domain.Models.Features.RiskManagement.RiskValues.Args;

public class CreateRiskValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Color { get; set; }
    public string? Condition { get; set; }
    public float NumericValue { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long StrategyId { get; set; }
}