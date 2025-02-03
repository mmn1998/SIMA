namespace SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;

public class GetCobitScenarioQueryResult
{
    public long Id { get; set; }
    public long CobitScenarioCategoryId { get; set; }
    public string? CobitScenarioCategoryName { get; set; }
    public long ScenarioId { get; set; }
    public string? ScenarioName { get; set; }
    public long ActiveStatusId { get; set; }
    public string? CobitIdentifier { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ActiveStatus { get; set; }
}