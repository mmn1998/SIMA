namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class ModifyUserLocationArg
{
    public long Id { get; set; }

    public long? UserId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ActiveStatusId { get; set; }

    public long? ModifiedBy { get; set; }
    public long? LocationId { get; set; }
}
