namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Args;

public class CreateInherentOccurrenceProbabilityValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}