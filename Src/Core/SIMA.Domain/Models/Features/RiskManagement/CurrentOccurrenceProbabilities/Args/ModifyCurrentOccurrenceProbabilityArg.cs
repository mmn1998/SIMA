namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Args;

public class ModifyCurrentOccurrenceProbabilityArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long FrequencyId { get; set; }
    public long CurrentOccurrenceProbabilityValueId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}