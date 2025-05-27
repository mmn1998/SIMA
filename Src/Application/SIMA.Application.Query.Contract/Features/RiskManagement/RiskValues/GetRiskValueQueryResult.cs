namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskValues;

public class GetRiskValueQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Condition { get; set; }
    public string? Color { get; set; }
    public float? NumericValue { get; set; }
    public string? ActiveStatus { get; set; }
}