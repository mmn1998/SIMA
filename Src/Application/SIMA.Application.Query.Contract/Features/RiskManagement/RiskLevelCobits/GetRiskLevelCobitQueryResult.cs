namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelCobits;

public class GetRiskLevelCobitQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long? SeverityId { get; set; }
    public string? SeverityName { get; set; }
    public string? SeverityValueName { get; set; }
    public long? CurrentOccurrenceProbabilityValueId { get; set; }
    public string? CurrentOccurrenceProbabilityValueName { get; set; }
    public float? NumericValue { get; set; }
    public string? ActiveStatus { get; set; }
}