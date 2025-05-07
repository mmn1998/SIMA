namespace SIMA.Domain.Models.Features.BCP.Consequences.Args;

public class ModifyConsequenceArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
