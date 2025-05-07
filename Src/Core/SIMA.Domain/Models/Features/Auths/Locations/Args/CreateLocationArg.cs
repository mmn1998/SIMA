namespace SIMA.Domain.Models.Features.Auths.Locations.Args;

public class CreateLocationArg
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long? ParentId { get; set; }
    public long? LocationTypeId { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
