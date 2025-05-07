namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class CreateUserLocationAccessArg
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public long? LocationId { get; set; }

    public DateOnly? ActiveFrom { get; set; }

    public DateOnly? ActiveTo { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long ActiveStatusId { get; set; }


    public long? CreatedBy { get; set; }
}
