namespace SIMA.Domain.Models.Features.Auths.Groups.Args;

public class CreateUserGroupArg
{

    public long? UserId { get; set; }
    public long? GroupId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateOnly? ActiveFrom { get; set; }
    public DateOnly? ActiveTo { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}
