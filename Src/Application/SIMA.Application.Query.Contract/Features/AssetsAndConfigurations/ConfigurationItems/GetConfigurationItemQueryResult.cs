using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;

public class GetConfigurationItemQueryResult
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public long OwnerId { get; set; }
    public string? OwnerName { get; set; }
    public string? VersionNumber { get; set; }
    public DateTime? ReleaseDate { get; set; } 
    public string? Description { get; set; }
    public DateTime? LastUpdateDate { get; set; } 
    public long ConfigurationItemTypeId { get; set; }
    public string? ConfigurationItemTypeName { get; set; }
    public long ConfigurationItemStatusId { get; set; }
    public string? ConfigurationItemStatusName { get; set; }
    public long LicenseSupplierId { get; set; }
    public string? LicenseSupplierName { get; set; }
    public long TimeMeasurementId { get; set; }
    public string? TimeMeasurementName { get; set; }
    public float Uptime { get; set; }
    public float Mttr { get; set; }
    public float Mtbf { get; set; }
    public long SupplierId { get; set; }
    public string? SupplierName { get; set; }
    public long BusinessCriticalityId { get; set; }
    public string? BusinessCriticalityName { get; set; }
    public long CompanyBuildingLocationId { get; set; }
    public string? CompanyBuildingLocationName { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatusId { get; set; }
    public string? ActiveStatusName { get; set; }
    public long? IssueId { get; set; }
    public string? IssueCode { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedAtPersian => CreatedAt.ToPersianDateTime();
    public string? ReleaseDatePersian => ReleaseDate.ToPersianDateTime();
    public string? LastUpdateDatePersian => LastUpdateDate.ToPersianDateTime();
    public string? CreatedBy { get; set; }
    
}
