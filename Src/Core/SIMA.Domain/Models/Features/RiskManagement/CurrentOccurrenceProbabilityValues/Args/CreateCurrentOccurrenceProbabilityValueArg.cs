namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Args;

public class CreateCurrentOccurrenceProbabilityValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ValuingIntervalTitle { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}