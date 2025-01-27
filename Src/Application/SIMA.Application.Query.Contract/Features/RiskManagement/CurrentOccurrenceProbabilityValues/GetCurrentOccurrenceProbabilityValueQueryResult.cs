namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;

public class GetCurrentOccurrenceProbabilityValueQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ValuingIntervalTitle { get; set; }
    public string? Color { get; set; }
    public float? NumericValue { get; set; }
    public string? ActiveStatus { get; set; }
}