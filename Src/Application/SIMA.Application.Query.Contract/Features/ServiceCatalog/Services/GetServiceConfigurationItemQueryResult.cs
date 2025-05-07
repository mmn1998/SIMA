namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;

public class GetServiceConfigurationItemQueryResult
{
    public long Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public long? OwnerId { get; set; }
    public string? OwnerFullName { get; set; }
    public long? SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public string? SupplierCode { get; set; }
    public long? ConfigurationItemTypeId { get; set; }
    public string? ConfigurationItemTypeName { get; set; }
    public string? ConfigurationItemTypeCode { get; set; }
    public long? ConfigurationItemStatusId { get; set; }
    public string? ConfigurationItemStatusName { get; set; }
    public string? ConfigurationItemStatusCode { get; set; }
    public long? LicenseTypeId { get; set; }
    public string? LicenseTypeName { get; set; }
    public string? LicenseTypeCode { get; set; }
    public long? LicenseSupplierId { get; set; }
    public string? LicenseSupplierName { get; set; }
    public string? LicenseSupplierCode { get; set; }
    public long? CompanyBuildingLocationId { get; set; }
    public string? CompanyBuildingLocationName { get; set; }
    public string? CompanyBuildingLocationCode { get; set; }
}
