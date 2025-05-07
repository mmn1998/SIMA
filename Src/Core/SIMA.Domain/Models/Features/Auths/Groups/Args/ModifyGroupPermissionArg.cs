namespace SIMA.Domain.Models.Features.Auths.Groups.Args;

public class ModifyGroupPermissionArg
{
    public long Id { get; set; }
    public long GroupId { get; set; }
    public long PermissionId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ActiveStatusId { get; set; }

    public long? ModifiedBy { get; set; }
}
