using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItem : Entity, IAggregateRoot
{
    private ConfigurationItem() { }
    private ConfigurationItem(CreateConfigurationItemArg arg)
    {
        Id = new(arg.Id);
        OwnerId = new(arg.OwnerId);
        ConfigurationItemTypeId = new(arg.ConfigurationItemTypeId);
        ConfigurationItemStatusId = new(arg.ConfigurationItemStatusId);
        LicenseTypeId = new(arg.LicenseTypeId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        CompanyBuildingLocationId = new(arg.CompanyBuildingLocationId);
        Code = arg.Code;
        Title = arg.Title;
        Description = arg.Description;
        VersionNumber = arg.VersionNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        ReleaseDate = arg.ReleaseDate;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationItem> Create(CreateConfigurationItemArg arg, IConfigurationItemDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConfigurationItem(arg);
    }
    public async Task Modify(ModifyConfigurationItemArg arg, IConfigurationItemDomainService service)
    {
        await ModifyGuards(arg, service);
        OwnerId = new(arg.OwnerId);
        ConfigurationItemTypeId = new(arg.ConfigurationItemTypeId);
        ConfigurationItemStatusId = new(arg.ConfigurationItemStatusId);
        LicenseTypeId = new(arg.LicenseTypeId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        CompanyBuildingLocationId = new(arg.CompanyBuildingLocationId);
        Code = arg.Code;
        Title = arg.Title;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemArg arg, IConfigurationItemDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.VersionNumber.NullCheck();
        if (arg.Title.Length > 1000) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsVersionUnique(arg.VersionNumber)) throw new SimaResultException(CodeMessges._100115Code, Messages.UniqueVersionError);
    }
    private async Task ModifyGuards(ModifyConfigurationItemArg arg, IConfigurationItemDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.VersionNumber.NullCheck();
        if (arg.Title.Length > 1000) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsVersionUnique(arg.VersionNumber, Id)) throw new SimaResultException(CodeMessges._100115Code, Messages.UniqueVersionError);
    }
    #endregion
    public ConfigurationItemId Id { get; private set; }
    public StaffId OwnerId { get; private set; }
    public virtual Staff Owner { get; private set; }
    public ConfigurationItemTypeId ConfigurationItemTypeId { get; private set; }
    public virtual ConfigurationItemType ConfigurationItemType { get; private set; }
    public ConfigurationItemStatusId ConfigurationItemStatusId { get; private set; }
    public virtual ConfigurationItemStatus ConfigurationItemStatus { get; private set; }
    public LicenseTypeId LicenseTypeId { get; private set; }
    public virtual LicenseType LicenseType { get; private set; }
    public SupplierId? SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }
    public DataCenterId? DataCenterId { get; private set; }
    public virtual DataCenter? DataCenter { get; private set; }
    public SupplierId? LicenseSupplierId { get; private set; }
    public virtual Supplier? LicenseSupplier { get; private set; }
    public LocationId CompanyBuildingLocationId { get; private set; }
    public virtual Location CompanyBuildingLocation { get; private set; }
    public string? Code { get; private set; }
    public string? Title { get; private set; }
    public string? VersionNumber { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public string? Description { get; private set; }
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
    private List<ConfigurationItemChangeOwnerHistory> _configurationItemChangeOwnerHistories = new();
    public ICollection<ConfigurationItemChangeOwnerHistory> ConfigurationItemChangeOwnerHistories => _configurationItemChangeOwnerHistories;
    private List<ConfigurationItemVersioning> _configurationItemVersionings = new();
    public ICollection<ConfigurationItemVersioning> ConfigurationItemVersionings => _configurationItemVersionings;
    private List<ConfigurationItemChangeStatusHistory> _configurationItemChangeStatusHistories = new();
    public ICollection<ConfigurationItemChangeStatusHistory> ConfigurationItemChangeStatusHistories => _configurationItemChangeStatusHistories;

    private List<ServiceConfigurationItem> _serviceConfigurationItems = new();
    public ICollection<ServiceConfigurationItem> ServiceConfigurationItems => _serviceConfigurationItems;
    private List<CriticalActivityConfigurationItem> _criticalActivityConfigurationItems = new();
    public ICollection<CriticalActivityConfigurationItem> CriticalActivityConfigurationItems => _criticalActivityConfigurationItems;
    private List<ConfigurationItemApi> _configurationItemApis = new();
    public ICollection<ConfigurationItemApi> ConfigurationItemApis => _configurationItemApis;
    private List<ConfigurationItemRelationship> _configurationItemRelationships = new();
    public ICollection<ConfigurationItemRelationship> ConfigurationItemRelationships => _configurationItemRelationships;
    private List<ConfigurationItemRelationship> _relatedConfigurationItemRelationships = new();
    public ICollection<ConfigurationItemRelationship> RelatedConfigurationItemRelationships => _relatedConfigurationItemRelationships;
    private List<ConfigurationItemAccessInfo> _configurationItemAccessInfos = new();
    public ICollection<ConfigurationItemAccessInfo> ConfigurationItemAccessInfos => _configurationItemAccessInfos;
    private List<ConfigurationItemCustomFieldValue> _configurationItemCustomFieldValues = new();
    public ICollection<ConfigurationItemCustomFieldValue> ConfigurationItemCustomFieldValues => _configurationItemCustomFieldValues;
    private List<ConfigurationItemBackupSchedule> _configurationItemBackupSchedules = new();
    public ICollection<ConfigurationItemBackupSchedule> ConfigurationItemBackupSchedules => _configurationItemBackupSchedules;
    private List<ConfigurationItemBackupSchedule> _backupConfigurationItemBackupSchedules = new();
    public ICollection<ConfigurationItemBackupSchedule> BackupConfigurationItemBackupSchedules => _backupConfigurationItemBackupSchedules;
    
    private List<ConfigurationItemDataProcedure> _configurationItemDataProcedure = new();
    public ICollection<ConfigurationItemDataProcedure> ConfigurationItemDataProcedures => _configurationItemDataProcedure;
    
    private List<DataProcedure> _dataProcedure = new();
    public ICollection<DataProcedure> DataProcedure => _dataProcedure;
    
    private List<ConfigurationItemAsset> _configurationItemAssets = new();
    public ICollection<ConfigurationItemAsset> ConfigurationItemAssets => _configurationItemAssets;
    
    private List<ConfigurationItemAssetHistory> _configurationItemAssetHistories = new();
    public ICollection<ConfigurationItemAssetHistory> ConfigurationItemAssetHistories => _configurationItemAssetHistories;
    
    private List<ConfigurationItemDocument> _configurationItemDocuments = new();
    public ICollection<ConfigurationItemDocument> ConfigurationItemDocuments => _configurationItemDocuments;
    private List<ConfigurationItemCustomFieldOption> _configurationItemCustomFieldOption = new();
    public ICollection<ConfigurationItemCustomFieldOption> ConfigurationItemCustomFieldOption => _configurationItemCustomFieldOption;
    
    
    private List<ConfigurationItemIssue> _configurationItemIssues = new();
    public ICollection<ConfigurationItemIssue> ConfigurationItemIssues => _configurationItemIssues;
    
}
