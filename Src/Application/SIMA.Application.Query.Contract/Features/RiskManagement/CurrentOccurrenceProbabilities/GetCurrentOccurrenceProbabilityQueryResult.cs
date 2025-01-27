namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class GetCurrentOccurrenceProbabilityQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long? ScenarioHistoryId { get; set; }
    public string? ScenarioHistoryName { get; set; }
    public long? InherentOccurrenceProbabilityValueId { get; set; }
    public string? InherentOccurrenceProbabilityValueName { get; set; }
    public long? MatrixAValueId { get; set; }
    public string? MatrixAValueName { get; set; }
    public string? ActiveStatus { get; set; }
}