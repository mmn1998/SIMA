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
        public OwnerInfo? OwnerInfo { get; set; }
        public DataCenterInfo? DataCenterInfo { get; set; }
        public ConfigurationItemTypeInfo? ConfigurationItemTypeInfo { get; set; }
        public ConfigurationItemStatusInfo? ConfigurationItemStatusInfo { get; set; }
        public LicenseTypeInfo? LicenseTypeInfo { get; set; }
        public LicenseSupplierInfo? LicenseSupplierInfo { get; set; }
        public SupplierInfo? SupplierInfo { get; set; }
        public CompanyBuildingLocation? CompanyBuildingLocation { get; set; }

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

}
