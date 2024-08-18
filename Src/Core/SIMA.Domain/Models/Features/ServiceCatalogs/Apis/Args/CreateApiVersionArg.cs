namespace SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Args;

public class CreateApiVersionArg
{
    public long Id { get; set; }
    public long ApiId { get; set; }
    public string VersionNumber { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string IsCurrentVersion { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
