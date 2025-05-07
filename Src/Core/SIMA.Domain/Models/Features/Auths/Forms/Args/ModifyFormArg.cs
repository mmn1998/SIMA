namespace SIMA.Domain.Models.Features.Auths.Forms.Args;

public class ModifyFormArg
{
    public string? JsonContent { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}


