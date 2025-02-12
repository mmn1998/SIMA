namespace SIMA.Domain.Models.Features.BCP.ConsequenceValues.Args;

public class ModifyConsequenceValueArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long ConsequenceId { get; set; }
    public long OriginId { get; set; }
    public long ActiveStatusId { get; set; }
    public float ValueNumber { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}