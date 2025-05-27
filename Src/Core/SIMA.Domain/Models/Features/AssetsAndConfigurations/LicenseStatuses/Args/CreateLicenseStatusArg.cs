namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Args;

public class CreateLicenseStatusArg
{
    public long Id { get; set; }
    public long ActiveStatusId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
