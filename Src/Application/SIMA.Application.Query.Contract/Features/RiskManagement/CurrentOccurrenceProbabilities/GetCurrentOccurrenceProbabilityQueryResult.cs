namespace SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;

public class GetCurrentOccurrenceProbabilityQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long? FrequencyId { get; set; }
    public string? FrequencyName { get; set; }
    public string? ValueTitle { get; set; }
    public long? InherentOccurrenceProbabilityValueId { get; set; }
    public string? InherentOccurrenceProbabilityValueName { get; set; }
    public long? CurrentOccurrenceProbabilityValueId { get; set; }
    public string? CurrentOccurrenceProbabilityValueName { get; set; }
    public string? ActiveStatus { get; set; }
}