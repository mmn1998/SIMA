namespace SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Args;

public class ModifySeverityValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? Color { get; set; }
    public string? ValuingIntervalTitle { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}