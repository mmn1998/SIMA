namespace SIMA.Application.Query.Contract.Features.RiskManagement.ScenarioHistories;

public class GetScenarioHistoryQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ValueTitle { get; set; }
    public float? NumericValue { get; set; }
    public string? ActiveStatus { get; set; }

}  
