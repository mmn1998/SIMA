namespace SIMA.Domain.Models.Features.Auths.Roles.Args;

public class ModifyRolePermissionArg
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public long PermissionId { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
