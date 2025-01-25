namespace SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Args;

public class ModifyInherentOccurrenceProbabilityArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long MatrixAValueId { get; set; }
    public long ScenarioHistoryId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}