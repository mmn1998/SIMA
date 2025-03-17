namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class CreateConfigurationItemArg
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public long ConfigurationItemTypeId { get; set; }
    public long ConfigurationItemStatusId { get; set; }
    public long LicenseTypeId { get; set; }
    public long? SupplierId { get; set; }
    public long? BusinessCriticalityId { get; set; }
    public string? Title { get;  set; }
    public string? VersionNumber { get;  set; }
    public long CompanyBuildingLocationId { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? UpdateSubject { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public long? CreatedBy { get; set; }
}