using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityAssetQueryResult
{
    public long Id { get; set; }
    public string? SerialNumber { get; set; }
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public DateTime? ManufactureDate { get; set; }
    public string? ManufactureDatePersian => ManufactureDate.ToPersianDate();
    public DateTime? OwnershipDate { get; set; }
    public string? OwnershipDatePersian => OwnershipDate.ToPersianDate();
    public DateTime? InServiceDate { get; set; }
    public string? InServiceDatePersian => InServiceDate.ToPersianDate();
    public DateTime? ExpireDate { get; set; }
    public string? ExpireDatePersian => ExpireDate.ToPersianDate();
    public DateTime? RetiredDate { get; set; }
    public string? RetiredDatePersian => RetiredDate.ToPersianDate();
    public string? Description { get; set; }
    public decimal? OwnershipPrepaymentValue { get; set; }
    public string? OwnershipPaymentValue { get; set; }
    public string? HasConfidentialInformation { get; set; }
    public long? OwnerId { get; set; }
    public string? OwnerFullName { get; set; }
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierCode { get; set; }
    public long? BusinessCriticalityId { get; set; }
    public string? BusinessCriticalityName { get; set; }
    public string? BusinessCriticalityCode { get; set; }
    public long? AssetTechnicalStatusId { get; set; }
    public string? AssetTechnicalStatusName { get; set; }
    public string? AssetTechnicalStatusCode { get; set; }
    public long? WarehouseId { get; set; }
    public string? WarehouseName { get; set; }
    public string? WarehouseCode { get; set; }
    public long? AssetPhysicalStatusId { get; set; }
    public string? AssetPhysicalStatusName { get; set; }
    public string? AssetPhysicalStatusCode { get; set; }
    public long? OwnershipTypeId { get; set; }
    public string? OwnershipTypeName { get; set; }
    public string? OwnershipTypeCode { get; set; }
    public long? PhysicalLocationId { get; set; }
    public string? PhysicalLocationName { get; set; }
    public string? PhysicalLocationCode { get; set; }
    public string? Title { get; set; }
}