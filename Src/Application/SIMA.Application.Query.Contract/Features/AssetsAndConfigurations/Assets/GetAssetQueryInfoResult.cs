using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Helper.FormMaker;

namespace SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets
{
    public class GetAssetQueryInfoResult
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? SerialNumber { get; set; }
        public string? Title { get; set; }
        public string? Model { get; set; }
        public string? VersionNumber { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public string? ManufactureDatePersian => ManufactureDate.ToPersianDate(); 
        public DateTime? OwnerShipDate { get; set; }
        public string? OwnerShipDatePersian => OwnerShipDate.ToPersianDate();
        public DateTime? InserviceDate { get; set; }
        public string? InserviceDatePersian => InserviceDate.ToPersianDate();
        public DateTime? ExpireDate { get; set; }
        public string? ExpireDatePersian => ExpireDate.ToPersianDate();
        public DateTime? RetiredDate { get; set; }
        public string? RetiredDatePersian => ExpireDate.ToPersianDate();
        public string? Description { get; set; }
        public decimal? OwnershipPaymentValue { get; set; }
        public decimal? OwnershipPrepaymentValue { get; set; }
        public string? HasConfidentialInformation { get; set; }
        public string? ActiveStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedAtPersianDate { get; set; }
        public string? CreatedBy { get; set; }
        public DataCenterInfo? DataCenterInfo { get; set; }
        public SupplierInfo? SupplierInfo { get; set; }
        public OwnerInfo? OwnerInfo { get; set; }
        public WareHouseInfo? WareHouseInfo { get; set; }
        public AssetTypeInfo? AssetTypeInfo { get; set; }
        public AssetCategoryInfo? AssetCategoryInfo { get; set; }
        public OprationStatusInfo? OprationStatusInfo { get; set; }
        public AssetTechnicalStatusInfo? AssetTechnicalStatusInfo { get; set; }
        public AssetPhysicalStatusInfo? AssetPhysicalStatusInfo { get; set; }
        public OwnerShipTypeInfo? OwnerShipTypeInfo { get; set; }
        public UserTypeInfo? UserTypeInfo { get; set; }
        public BusinessCriticalityInfo? BusinessCriticalityInfo { get; set; }
        public PhysicalLocationInfo? PhysicalLocationInfo { get; set; }
        //  new
        public IEnumerable<AssetCustomFeildValueInfo> AssetCustomFeildValue { get; set; }
        public IEnumerable<AssetCustomFeildOptionInfo> AssetCustomFeildOption { get; set; }
        public IEnumerable<ServiceAssetInfo> ServiceAsset { get; set; }
        // Added new collections
        public IEnumerable<AssetDocumentInfo> AssetDocument { get; set; } // Asset documents collection
        public IEnumerable<AssetAssignedStaffInfo> AssetAssignedStaff { get; set; } // Asset assigned staff collection  
        public IEnumerable<ConfigurationItemAssetInfo> ConfigurationItemAsset { get; set; } // Configuration items collection
        public IEnumerable<ComplexAssetInfo> ComplexAsset { get; set; } // Complex assets collection
    }
    
    // Added new classes for the collections
    public class AssetDocumentInfo
    {
        public long? DocumentId { get; set; }
        public long? DocumentTypeId { get; set; }
        public string? Title { get; set; }
        public long? DocumentExtentionId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class AssetAssignedStaffInfo 
    {
        public long? StaffId { get; set; }
        public string? StaffName { get; set; }
        public string? staffNumber { get; set; }
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long companyId { get; set; }
        public string? companyName { get; set; }
        public string? BranchId { get; set; }
        public string? BranchName { get; set; }
        public long ResponsibleTypeId { get; set; }
        public string? ResponsibleTypeName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ConfigurationItemAssetInfo
    {
        public long? Id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public string? versionNumber { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ComplexAssetInfo
    {
        public long? Id { get; set; }
        public string? serialNumber { get; set; }
        public string? name { get; set; }
        public string? Model { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ServiceAssetInfo
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }

    }
    public class AssetCustomFeildValueInfo
    {
        public long Id { get; set; }
        public long? AssetCustomFeildId { get; set; }
        public long? CustomFeildTypeId { get; set; }
        public string? CustomFeildTypeName { get; set; }
        public string? value { get; set; }
        public IEnumerable<AssetCustomFeildOptionInfo?> AssetCustomFeildOption { get; set; }
    }

    public class AssetCustomFeildOptionInfo
    {
        public long AssetCustomFeildValueId { get; set; }
        public long? Id { get; set; }
        public string? OptionValue { get; set; }
        public string? OptionText { get; set; }
    }


    public class DataCenterInfo
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

    public class WareHouseInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class AssetTypeInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class AssetCategoryInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class OprationStatusInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class AssetTechnicalStatusInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class AssetPhysicalStatusInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class OwnerShipTypeInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class UserTypeInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class BusinessCriticalityInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }

    public class PhysicalLocationInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

    }
}
