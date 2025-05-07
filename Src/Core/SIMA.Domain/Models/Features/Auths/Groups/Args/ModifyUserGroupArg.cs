namespace SIMA.Domain.Models.Features.Auths.Groups.Args;

public class ModifyUserGroupArg
{
    public long Id { get; set; }

    public long? UserId { get; set; }
    public long GroupId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ActiveStatusId { get; set; }

    public long? ModifiedBy { get; set; }
}
