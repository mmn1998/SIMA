namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class ModifyApiVersionArg
{
    public long ApiId { get; set; }
    public string? VersionNumber { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? IsCurrentVersion { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
