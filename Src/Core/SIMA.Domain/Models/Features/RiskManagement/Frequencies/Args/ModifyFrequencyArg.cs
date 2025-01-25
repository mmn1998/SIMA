namespace SIMA.Domain.Models.Features.RiskManagement.Frequencies.Args;

public class ModifyFrequencyArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}