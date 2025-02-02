namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Args;

public class ModifyRiskLevelCobitArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public float NumericValue { get; set; }
    public long SeverityId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}