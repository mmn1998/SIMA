namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class ModifyUserArg
{
    public long Id { get; set; }

    public long? ProfileId { get; set; }
    public string Username { get; set; }

    public string Password { get; set; }

    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
