namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Args;

public class CreateCurrentOccurrenceProbabilityArg
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public long InherentOccurrenceProbabilityValueId { get; set; }
    public long CurrentOccurrenceProbabilityValueId { get; set; }
    public long FrequencyId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}