namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class ModifyUserRoleArg
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ModifiedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
}
