namespace SIMA.Domain.Models.Features.Auths.Forms.Args;

public class CreateFormPermissionArg
{
    public long Id { get; set; }
    public long FormId { get; set; }
    public long PermissionId { get; set; }

    public DateTime ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
