using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class Asset : Entity
{
    private Asset() { }
    private Asset(CreateAssetArg arg)
    {
        Id = new(arg.Id);
        AssetTypeId = new(arg.AssetTypeId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        if (arg.OwnerId.HasValue) OwnerId = new(arg.OwnerId.Value);
        if (arg.WarehouseId.HasValue) WarehouseId = new(arg.WarehouseId.Value);
        if (arg.AssetTechnicalStatusId.HasValue) AssetTechnicalStatusId = new(arg.AssetTechnicalStatusId.Value);
        if (arg.AssetPhysicalStatusId.HasValue) AssetPhysicalStatusId = new(arg.AssetPhysicalStatusId.Value);
        if (arg.OwnershipTypeId.HasValue) OwnershipTypeId = new(arg.OwnershipTypeId.Value);
        if (arg.UserTypeId.HasValue) UserTypeId = new(arg.UserTypeId.Value);
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        SerialNumber = arg.SerialNumber;
        Model = arg.Model;
        Manufacturer = arg.Manufacturer;
        ManufactureDate = arg.ManufactureDate;
        OwnershipDate = arg.OwnershipDate;
        InServiceDate = arg.InServiceDate;
        ExpireDate = arg.ExpireDate;
        RetiredDate = arg.RetiredDate;
        Description = arg.Description;
        OwnershipPaymentValue = arg.OwnershipPaymentValue;
        OwnershipPrepaymentValue = arg.OwnershipPrepaymentValue;
        HasConfidentialInformation = arg.HasConfidentialInformation;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Asset> Create(CreateAssetArg arg, IAssetDomainService service)
    {
        await CreateGuards(arg, service);
        return new Asset(arg);
    }
    public async Task Modify(ModifyAssetArg arg, IAssetDomainService service)
    {
        await ModifyGuards(arg, service);
        AssetTypeId = new(arg.AssetTypeId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        if (arg.OwnerId.HasValue) OwnerId = new(arg.OwnerId.Value);
        if (arg.WarehouseId.HasValue) WarehouseId = new(arg.WarehouseId.Value);
        if (arg.AssetTechnicalStatusId.HasValue) AssetTechnicalStatusId = new(arg.AssetTechnicalStatusId.Value);
        if (arg.AssetPhysicalStatusId.HasValue) AssetPhysicalStatusId = new(arg.AssetPhysicalStatusId.Value);
        if (arg.OwnershipTypeId.HasValue) OwnershipTypeId = new(arg.OwnershipTypeId.Value);
        if (arg.UserTypeId.HasValue) UserTypeId = new(arg.UserTypeId.Value);
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        SerialNumber = arg.SerialNumber;
        Model = arg.Model;
        Manufacturer = arg.Manufacturer;
        ManufactureDate = arg.ManufactureDate;
        OwnershipDate = arg.OwnershipDate;
        InServiceDate = arg.InServiceDate;
        ExpireDate = arg.ExpireDate;
        RetiredDate = arg.RetiredDate;
        Description = arg.Description;
        OwnershipPaymentValue = arg.OwnershipPaymentValue;
        OwnershipPrepaymentValue = arg.OwnershipPrepaymentValue;
        HasConfidentialInformation = arg.HasConfidentialInformation;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateAssetArg arg, IAssetDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyAssetArg arg, IAssetDomainService service)
    {

    }
    #endregion
    public AssetId Id { get; private set; }
    public string? SerialNumber { get; private set; }
    public SupplierId? SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }
    public StaffId? OwnerId { get; private set; }
    public virtual Staff? Owner { get; private set; }
    public AssetTypeId AssetTypeId { get; private set; }
    public virtual AssetType AssetType { get; private set; }
    public WarehouseId? WarehouseId { get; private set; }
    public virtual Warehouse? Warehouse { get; private set; }
    public string? Model { get; private set; }
    public string? Manufacturer { get; private set; }
    public DateOnly? ManufactureDate { get; private set; }
    public DateOnly? OwnershipDate { get; private set; }
    public DateOnly? InServiceDate { get; private set; }
    public DateOnly? ExpireDate { get; private set; }
    public DateOnly? RetiredDate { get; private set; }
    public string? Description { get; private set; }
    public AssetTechnicalStatusId? AssetTechnicalStatusId { get; private set; }
    public virtual AssetTechnicalStatus? AssetTechnicalStatus { get; private set; }
    public AssetPhysicalStatusId? AssetPhysicalStatusId { get; private set; }
    public virtual AssetPhysicalStatus? AssetPhysicalStatus { get; private set; }
    public OwnershipTypeId? OwnershipTypeId { get; private set; }
    public virtual OwnershipType? OwnershipType { get; private set; }
    public decimal? OwnershipPrepaymentValue { get; private set; }
    public decimal? OwnershipPaymentValue { get; private set; }
    public UserTypeId? UserTypeId { get; private set; }
    public virtual UserType? UserType { get; private set; }
    public BusinessCriticalityId? BusinessCriticalityId { get; private set; }
    public virtual BusinessCriticality? BusinessCriticality { get; private set; }
    public LocationId PhysicalLocationId { get; private set; }
    public virtual Location PhysicalLocation { get; private set; }
    public string? HasConfidentialInformation { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<AssetIssue> _assetIssues = new();
    public ICollection<AssetIssue> AssetIssues => _assetIssues;
    private List<ServiceAsset> _serviceAssets = new();
    public ICollection<ServiceAsset> ServiceAssets => _serviceAssets;
    private List<AssetChangeTechnicalStatusHistory> _assetChangeTechnicalStatusHistories = new();
    public ICollection<AssetChangeTechnicalStatusHistory> AssetChangeTechnicalStatusHistories => _assetChangeTechnicalStatusHistories;
    private List<AssetChangePhysicalStatusHistory> _assetChangePhysicalStatusHistories = new();
    public ICollection<AssetChangePhysicalStatusHistory> AssetChangePhysicalStatusHistories => _assetChangePhysicalStatusHistories;
    private List<AssetDocument> _assetDocuments = new();
    public ICollection<AssetDocument> AssetDocuments => _assetDocuments;
    private List<AssetWarehouseHistory> _assetWarehouseHistories = new();
    public ICollection<AssetWarehouseHistory> AssetWarehouseHistories => _assetWarehouseHistories;
    private List<AssetChangeOwnerHistory> _assetChangeOwnerHistories = new();
    public ICollection<AssetChangeOwnerHistory> AssetChangeOwnerHistories => _assetChangeOwnerHistories;
    private List<ConfigurationItemAsset> _configurationItemAssets = new();
    public ICollection<ConfigurationItemAsset> ConfigurationItemAssets => _configurationItemAssets;
    private List<ConfigurationItemAssetHistory> _configurationItemAssetHistories = new();
    public ICollection<ConfigurationItemAssetHistory> ConfigurationItemAssetHistories => _configurationItemAssetHistories;

    private List<BusinessImpactAnalysisAsset> _businessImpactAnalysisAsset = new();
    public ICollection<BusinessImpactAnalysisAsset> BusinessImpactAnalysisAssets => _businessImpactAnalysisAsset;
    private List<CriticalActivityAsset> _criticalActivityAssets = new();
    public ICollection<CriticalActivityAsset> CriticalActivityAssets => _criticalActivityAssets;
    
}

