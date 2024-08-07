namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;

public class ModifyConfigurationItemArg
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public long ConfigurationItemTypeId { get; set; }
    public long ConfigurationItemStatusId { get; set; }
    public long LicenseTypeId { get; set; }
    public long? SupplierId { get; set; }
    public long CompanyBuildingLocationId { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
