using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems
{
    public class GetConfigurationItemQueryInfoResult 
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? VersionNumber { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ReleasePersianDate => ReleaseDate.ToPersianDateTime();
        public string? Description { get; set; }
        public long? LicenseType { get; set; }
        public string? ActiveStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersianDate => CreatedAt.ToPersianDateTime();
        public string? CreatedBy { get; set; }
        public float? Uptime { get; set; }
        public float? Mttr { get; set; }
        public float? Mtbf { get; set; }
        public long? TimeMeasurementId { get; set; }
        public string? TimeMeasurementName { get; set; }
        public OwnerInfo? OwnerInfo { get; set; }
        public DataCenterInfo? DataCenterInfo { get; set; }
        public ConfigurationItemTypeInfo? ConfigurationItemTypeInfo { get; set; }
        public ConfigurationItemStatusInfo? ConfigurationItemStatusInfo { get; set; }
        public LicenseTypeInfo? LicenseTypeInfo { get; set; }      
        public SupplierInfo? SupplierInfo { get; set; }
        public CompanyBuildingLocation? CompanyBuildingLocation { get; set; }
        public LicenseSupplierInfo? LicenseSupplierInfo { get; set; }
        public BusinessCriticalityInfo? BusinessCriticalityInfo { get; set; }
        public IEnumerable<ConfigurationItemCustomFieldValueList>? ConfigurationItemCustomFieldValueList { get; set; }
        public IEnumerable<ConfigurationItemSupportTeamList>? ConfigurationItemSupportTeamList { get; set; }
        public IEnumerable<ConfigurationItemAccessList>? ConfigurationItemAccessList { get; set; }
        public IEnumerable<ConfigurationItemBackupScheduleList>? ConfigurationItemBackupScheduleList { get; set; }
        public IEnumerable<ConfigurationItemApiList>? ConfigurationItemApiList { get; set; }
        public IEnumerable<ConfigurationItemDataProcedureList>? ConfigurationItemDataProcedureList { get; set; }
        public IEnumerable<ServiceConfigurationItemList>? ServiceConfigurationItemList { get; set; }
        public IEnumerable<ConfigurationItemAssetList>? ConfigurationItemAssetList { get; set; }
        public IEnumerable<ConfigurationItemDocumentList>? ConfigurationItemDocumentList { get; set; }
    

    }

    public class OwnerInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? StaffNumber { get; set; }
        public string? CompanyName { get; set; }
        public long? CompanyId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public string? BranchName { get; set; }
        public long? BranchId { get; set; }

    }

    public class DataCenterInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class ConfigurationItemTypeInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class ConfigurationItemStatusInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class LicenseTypeInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class LicenseSupplierInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class SupplierInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class CompanyBuildingLocation
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class BusinessCriticality
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class ConfigurationItemCustomFieldValueList
    {
        public long Id { get; set; }
        public string? ItemValue { get; set; }

    }

    public class ConfigurationItemSupportTeamList
    {
        public long Id { get; set; }
        public long MainStaffId { get; set; }
        public long MainDepartmentId { get; set; }
        public long MainBranchId { get; set; }
        public long SubsitutedStaffId { get; set; }
        public long SubsitutedDepartmentId { get; set; }
        public long SubsitutedBranchId { get; set; }

    }

    public class ConfigurationItemAccessList
    {
        public long Id { get; set; }
        public string? IPAddressFrom { get; set; }
        public string? IPAddressTo { get; set; }
        public string? PortFrom { get; set; }
        public string? PortTo { get; set; }
        public string? ActiveFrom { get; set; }
        public string? ActiveTo { get; set; }
        public long ActiveStatusId { get; set; }
      

    }

    public class ConfigurationItemBackupScheduleList
    {
        public long Id { get; set; }
        public long BackupConfigurationItemId { get; set; }
        public long DataCenterId { get; set; }
        public long BackupMethodId { get; set; }
        public long TimeMeasurementId { get; set; }
        public TimeSpan? StartTime { get; set; }
        public float? Duration { get; set; }
        public DateTime? LastTestDate { get; set; }
        public string? PersianLastTestDate => DateHelper.ToPersianDate(LastTestDate);
        public long ActiveStatusId { get; set; }


    }

    public class ConfigurationItemApiList
    {
        public long Id { get; set; }
      
    }

    public class ConfigurationItemDataProcedureList
    {
        public long Id { get; set; }

    }

    public class ServiceConfigurationItemList
    {
        public long Id { get; set; }

    }

    public class ConfigurationItemAssetList
    {
        public long Id { get; set; }

    }

    public class ConfigurationItemDocumentList
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public long ConfigurationItemId { get; set; }
        public long ActiveStatusId { get; set; }

    }

}
