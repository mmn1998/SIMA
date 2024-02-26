namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class ModifyUserPermissionArg
{
    public long? UserId { get; set; }
    public long Id { get; set; }

    public long PermissionId { get; set; }
    public long ActiveStatusId { get; set; }

    public string? IsActive { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public int ModifiedBy { get; set; }
}
