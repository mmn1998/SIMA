namespace SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;

public class CreateApiMethodActionArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public long CreatedBy { get; set; }
}
