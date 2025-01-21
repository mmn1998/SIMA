namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetArg
{
    public long Id { get; set; }
    public string? SerialNumber { get; set; }
    public long? SupplierId { get; set; }
    public long? OwnerId { get; set; }
    public long AssetTypeId { get; set; }
    public long? WarehouseId { get; set; }
    public string? Model { get; set; }
    public string? Title { get; set; }
    public string? Manufacturer { get; set; }
    public DateOnly? ManufactureDate { get; set; }
    public DateOnly? OwnershipDate { get; set; }
    public DateOnly? InServiceDate { get; set; }
    public DateOnly? ExpireDate { get; set; }
    public DateOnly? RetiredDate { get; set; }
    public string? Description { get; set; }
    public long? AssetTechnicalStatusId { get; set; }
    public long? AssetPhysicalStatusId { get; set; }
    public long? OwnershipTypeId { get; set; }
    public decimal? OwnershipPrepaymentValue { get; set; }
    public decimal? OwnershipPaymentValue { get; set; }
    public long? UserTypeId { get; set; }
    public long? BusinessCriticalityId { get; set; }
    public long? PhysicalLocationId { get; set; }
    public string? HasConfidentialInformation { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
