namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class ModifyUserDomainArg
{
    public long Id { get; set; }

    public long? UserId { get; set; }
    public long? DomainId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ActiveStatusId { get; set; }
    public long? ModifiedBy { get; set; }
}
