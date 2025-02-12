namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Args;

public class ModifyConsequenceIntensionArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float ValueNumber { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}