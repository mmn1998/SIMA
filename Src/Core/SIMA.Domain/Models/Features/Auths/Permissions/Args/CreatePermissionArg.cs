namespace SIMA.Domain.Models.Features.Auths.Permissions.Args;

public class CreatePermissionArg
{
    public long Id { get; set; }

    public long DomainId { get; set; }

    public string? Name { get; set; }

    public string? EnglishKey { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
