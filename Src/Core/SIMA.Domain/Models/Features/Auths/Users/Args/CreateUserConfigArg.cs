namespace SIMA.Domain.Models.Features.Auths.Users.Args;

public class CreateUserConfigArg
{
    public long Id { get; set; }

    public int UserId { get; set; }

    public long ConfigurationId { get; set; }

    public string? KeyValue { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
