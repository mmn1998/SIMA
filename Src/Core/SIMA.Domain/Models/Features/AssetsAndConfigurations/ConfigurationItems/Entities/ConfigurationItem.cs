using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Events;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        if (arg.TimeMeasurementId.HasValue) TimeMeasurementId = new(arg.TimeMeasurementId.Value);
        if (arg.LicenseStatusId.HasValue) LicenseStatusId = new(arg.LicenseStatusId.Value);
        CompanyBuildingLocationId = new(arg.CompanyBuildingLocationId);
        Code = arg.Code;
        Title = arg.Title;
        Description = arg.Description;
        VersionNumber = arg.VersionNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        ReleaseDate = arg.ReleaseDate;
        CreatedBy = arg.CreatedBy;
        LastUpdateDate = arg.LastUpdateDate;
        UpdateSubject = arg.UpdateSubject;
        Uptime = arg.Uptime;
        Mttr = arg.Mttr;
        Mtbf = arg.Mtbf;
        AddDomainEvent(new CreateConfigurationItemEvent
        (issueId: arg.IssueId, mainAggregateType: MainAggregateEnums.ConfigurationItem,
            name: arg.Title, sourceId: arg.Id, issuePriorityId: arg.IssuePriorityId, issueWeightCategoryId: arg.IssueWeightCategoryId));
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
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        if (arg.TimeMeasurementId.HasValue) TimeMeasurementId = new(arg.TimeMeasurementId.Value);
        if (arg.LicenseStatusId.HasValue) LicenseStatusId = new(arg.LicenseStatusId.Value);
        CompanyBuildingLocationId = new(arg.CompanyBuildingLocationId);
        Code = arg.Code;
        Title = arg.Title;
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        LastUpdateDate = arg.LastUpdateDate;
        UpdateSubject = arg.UpdateSubject;
        Uptime = arg.Uptime;
        Mttr = arg.Mttr;
        Mtbf = arg.Mtbf;
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
    public BusinessCriticalityId? BusinessCriticalityId { get; private set; }
    public virtual BusinessCriticality? BusinessCriticality { get; private set; }
    public virtual Supplier? Supplier { get; private set; }
    public DataCenterId? DataCenterId { get; private set; }
    public virtual DataCenter? DataCenter { get; private set; }
    public SupplierId? LicenseSupplierId { get; private set; }
    public virtual Supplier? LicenseSupplier { get; private set; }
    public LicenseStatusId? LicenseStatusId { get; private set; }
    public virtual LicenseStatus? LicenseStatus { get; private set; }
    public TimeMeasurementId? TimeMeasurementId { get; private set; }
    public virtual TimeMeasurement? TimeMeasurement { get; private set; }
    public LocationId CompanyBuildingLocationId { get; private set; }
    public virtual Location CompanyBuildingLocation { get; private set; }
    public string? Code { get; private set; }
    public string? Title { get; private set; }
    public float? Uptime { get; private set; }
    public float? Mttr { get; private set; }
    public float? Mtbf { get; private set; }
    public string? VersionNumber { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public DateTime? LastUpdateDate { get; private set; }
    public string? Description { get; private set; }
    public string? UpdateSubject { get; private set; }
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
        #region DeleteRelatedEntities
        DeleteConfigurationItemCustomFieldValues(userId);
        DeleteConfigurationItemSupportTeams(userId);
        DeleteConfigurationItemAccessInfos(userId);
        DeleteConfigurationItemBackupSchedules(userId);
        DeleteConfigurationItemApis(userId);
        DeleteConfigurationItemDataProcedures(userId);
        DeleteServiceConfigurationItems(userId);
        DeleteConfigurationItemDocuments(userId);
        DeleteConfigurationItemAssets(userId);
        #endregion
    }
    #region AddMethods
    public void AddConfigurationItemCustomFieldValues(List<CreateConfigurationItemCustomFieldValueArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemCustomFieldValue.Create(arg);
            _configurationItemCustomFieldValues.Add(entity);
        }
    }
    public void AddConfigurationItemSupportTeams(List<CreateConfigurationItemSupportTeamArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemSupportTeam.Create(arg);
            _configurationItemSupportTeams.Add(entity);
        }
    }
    public void AddConfigurationItemAccessInfos(List<CreateConfigurationItemAccessInfoArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemAccessInfo.Create(arg);
            _configurationItemAccessInfos.Add(entity);
        }
    }
    public void AddConfigurationItemBackupSchedules(List<CreateConfigurationItemBackupScheduleArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemBackupSchedule.Create(arg);
            _configurationItemBackupSchedules.Add(entity);
        }
    }
    public void AddConfigurationItemApis(List<CreateConfigurationItemApiArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemApi.Create(arg);
            _configurationItemApis.Add(entity);
        }
    }
    public void AddConfigurationItemDataProcedures(List<CreateConfigurationItemDataProcedureArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemDataProcedure.Create(arg);
            _configurationItemDataProcedure.Add(entity);
        }
    }
    public void AddServiceConfigurationItems(List<CreateServiceConfigurationItemArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceConfigurationItem.Create(arg);
            _serviceConfigurationItems.Add(entity);
        }
    }
    public void AddConfigurationItemDocuments(List<CreateConfigurationItemDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemDocument.Create(arg);
            _configurationItemDocuments.Add(entity);
        }
    }
    public void AddConfigurationItemAssets(List<CreateConfigurationItemAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemAsset.Create(arg);
            _configurationItemAssets.Add(entity);
        }
    }

    public void AddConfigurationIteIssues(List<CreateConfigurationItemIssueArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemIssue.Create(arg);
            _configurationItemIssues.Add(entity);
        }
    }
    #endregion
    #region ModifyMethods
    public void ModifyConfigurationItemCustomFieldValues(List<CreateConfigurationItemCustomFieldValueArg> args)
    {
        var activeEntities = _configurationItemCustomFieldValues.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value && c.ItemValue == x.ItemValue));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemCustomFieldValues.FirstOrDefault(x => x.AssetId.Value == arg.AssetId && x.ItemValue == arg.ItemValue && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemCustomFieldValue.Create(arg);
                _configurationItemCustomFieldValues.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemSupportTeams(List<CreateConfigurationItemSupportTeamArg> args)
    {
        var activeEntities = _configurationItemSupportTeams.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.MainStaffId == x.MainStaffId.Value && c.SubsitutedStaffId == x.SubsitutedStaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.MainStaffId.Value == x.MainStaffId && c.SubsitutedStaffId.Value == x.SubsitutedStaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemSupportTeams.FirstOrDefault(x => x.MainStaffId.Value == arg.MainStaffId && x.SubsitutedStaffId.Value == arg.SubsitutedStaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemSupportTeam.Create(arg);
                _configurationItemSupportTeams.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemAccessInfos(List<CreateConfigurationItemAccessInfoArg> args)
    {
        var activeEntities = _configurationItemAccessInfos.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.IPAddressTo == x.IPAddressTo && c.IPAddressTo == x.IPAddressTo && c.PortTo == x.PortTo && c.PortFrom == x.PortFrom));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.IPAddressTo == x.IPAddressTo && c.IPAddressFrom == x.IPAddressFrom && c.PortTo == x.PortTo && c.PortFrom == x.PortFrom));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemAccessInfos.FirstOrDefault(x => x.IPAddressTo == arg.IPAddressTo && x.IPAddressFrom == arg.IPAddressFrom && x.PortFrom == arg.PortFrom && x.PortTo == arg.PortTo && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemAccessInfo.Create(arg);
                _configurationItemAccessInfos.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemBackupSchedules(List<CreateConfigurationItemBackupScheduleArg> args)
    {
        var activeEntities = _configurationItemBackupSchedules.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.BackupConfigurationItemId == x.BackupConfigurationItemId.Value && c.BackupMethodId == x.BackupMethodId.Value && c.DataCenterId == x.DataCenterId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.BackupConfigurationItemId.Value == x.BackupConfigurationItemId && c.BackupMethodId.Value == x.BackupMethodId && c.DataCenterId.Value == x.DataCenterId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemBackupSchedules.FirstOrDefault(x => x.BackupConfigurationItemId.Value == arg.BackupConfigurationItemId && x.BackupMethodId.Value == arg.BackupMethodId && x.DataCenterId.Value == arg.DataCenterId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemBackupSchedule.Create(arg);
                _configurationItemBackupSchedules.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemApis(List<CreateConfigurationItemApiArg> args)
    {
        var activeEntities = _configurationItemApis.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ApiId == x.ApiId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ApiId.Value == x.ApiId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemApis.FirstOrDefault(x => x.ApiId.Value == arg.ApiId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemApi.Create(arg);
                _configurationItemApis.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemDataProcedures(List<CreateConfigurationItemDataProcedureArg> args)
    {
        var activeEntities = _configurationItemDataProcedure.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DataProcedureId == x.DataProcedureId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DataProcedureId.Value == x.DataProcedureId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemDataProcedure.FirstOrDefault(x => x.DataProcedureId.Value == arg.DataProcedureId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemDataProcedure.Create(arg);
                _configurationItemDataProcedure.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyServiceConfigurationItems(List<CreateServiceConfigurationItemArg> args)
    {
        var activeEntities = _serviceConfigurationItems.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceId == x.ServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceId.Value == x.ServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceConfigurationItems.FirstOrDefault(x => x.ServiceId.Value == arg.ServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceConfigurationItem.Create(arg);
                _serviceConfigurationItems.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemDocuments(List<CreateConfigurationItemDocumentArg> args)
    {
        var activeEntities = _configurationItemDocuments.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemDocument.Create(arg);
                _configurationItemDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    public void ModifyConfigurationItemAssets(List<CreateConfigurationItemAssetArg> args)
    {
        var activeEntities = _configurationItemAssets.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetId == x.AssetId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetId.Value == x.AssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemAssets.FirstOrDefault(x => x.AssetId.Value == arg.AssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ConfigurationItemAsset.Create(arg);
                _configurationItemAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteConfigurationItemCustomFieldValues(long userId)
    {
        foreach (var entity in _configurationItemCustomFieldValues)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemSupportTeams(long userId)
    {
        foreach (var entity in _configurationItemSupportTeams)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemAccessInfos(long userId)
    {
        foreach (var entity in _configurationItemAccessInfos)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemBackupSchedules(long userId)
    {
        foreach (var entity in _configurationItemBackupSchedules)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemApis(long userId)
    {
        foreach (var entity in _configurationItemApis)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemDataProcedures(long userId)
    {
        foreach (var entity in _configurationItemDataProcedure)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteServiceConfigurationItems(long userId)
    {
        foreach (var entity in _serviceConfigurationItems)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemDocuments(long userId)
    {
        foreach (var entity in _configurationItemDocuments)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteConfigurationItemAssets(long userId)
    {
        foreach (var entity in _configurationItemAssets)
        {
            entity.Delete(userId);
        }
    }
    #endregion
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


    private List<ConfigurationItemIssue> _configurationItemIssues = new();
    public ICollection<ConfigurationItemIssue> ConfigurationItemIssues => _configurationItemIssues;

    private List<ConfigurationItemSupportTeam> _configurationItemSupportTeams = new();
    public ICollection<ConfigurationItemSupportTeam> ConfigurationItemSupportTeams => _configurationItemSupportTeams;

}
