namespace SIMA.Domain.Models.Features.Auths.Groups.Args;

public class CreateGroupArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long ActiveStatusId { get; set; }
}
