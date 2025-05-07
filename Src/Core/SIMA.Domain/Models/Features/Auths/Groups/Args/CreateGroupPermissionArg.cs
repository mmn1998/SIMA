namespace SIMA.Domain.Models.Features.Auths.Groups.Args;

public class CreateGroupPermissionArg
{
    public long Id { get; set; }

    public long GroupId { get; set; }
    public long ActiveStatusId { get; set; }

    public long PermissionId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
