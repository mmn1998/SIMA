using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Categories.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataCenters.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.OperationalStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.Entities;
using SIMA.Domain.Models.Features.Auths.OwnershipTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.Entities;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetAssignedStaffs.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Events;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;
using SIMA.Framework.Common.Exceptions;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class Asset : Entity
{
    private Asset() { }
    private Asset(CreateAssetArg arg)
    {
        Id = new(arg.Id);
        AssetTypeId = new(arg.AssetTypeId);
        PhysicalLocationId = new(arg.PhysicalLocationId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        if (arg.OwnerId.HasValue) OwnerId = new(arg.OwnerId.Value);
        if (arg.WarehouseId.HasValue) WarehouseId = new(arg.WarehouseId.Value);
        if (arg.AssetTechnicalStatusId.HasValue) AssetTechnicalStatusId = new(arg.AssetTechnicalStatusId.Value);
        if (arg.AssetPhysicalStatusId.HasValue) AssetPhysicalStatusId = new(arg.AssetPhysicalStatusId.Value);
        if (arg.OwnershipTypeId.HasValue) OwnershipTypeId = new(arg.OwnershipTypeId.Value);
        if (arg.UserTypeId.HasValue) UserTypeId = new(arg.UserTypeId.Value);
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        if (arg.AssetCategoryId.HasValue) AssetCategoryId = new(arg.AssetCategoryId.Value);
        SerialNumber = arg.SerialNumber;
        Model = arg.Model;
        Manufacturer = arg.Manufacturer;
        Title = arg.Title;
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
        Code = arg.Code;
        VersionNumber = arg.VersionNumber;
        if (arg.DataCenterId.HasValue) DataCenterId = new(arg.DataCenterId.Value);
        if (arg.OperationalStatusId.HasValue) OperationalStatusId = new(arg.OperationalStatusId.Value);
        AddDomainEvent(new CreateAssetEvent
        (issueId: arg.IssueId, mainAggregateType: MainAggregateEnums.Asset,
            name: arg.Title, sourceId: arg.Id, issuePriorityId: arg.IssuePriorityId, issueWeightCategoryId: arg.IssueWeightCategoryId));
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
        PhysicalLocationId = new(arg.PhysicalLocationId);
        if (arg.SupplierId.HasValue) SupplierId = new(arg.SupplierId.Value);
        if (arg.OwnerId.HasValue) OwnerId = new(arg.OwnerId.Value);
        if (arg.WarehouseId.HasValue) WarehouseId = new(arg.WarehouseId.Value);
        if (arg.AssetTechnicalStatusId.HasValue) AssetTechnicalStatusId = new(arg.AssetTechnicalStatusId.Value);
        if (arg.AssetPhysicalStatusId.HasValue) AssetPhysicalStatusId = new(arg.AssetPhysicalStatusId.Value);
        if (arg.OwnershipTypeId.HasValue) OwnershipTypeId = new(arg.OwnershipTypeId.Value);
        if (arg.UserTypeId.HasValue) UserTypeId = new(arg.UserTypeId.Value);
        if (arg.BusinessCriticalityId.HasValue) BusinessCriticalityId = new(arg.BusinessCriticalityId.Value);
        if (arg.AssetCategoryId.HasValue) AssetCategoryId = new(arg.AssetCategoryId.Value);
        SerialNumber = arg.SerialNumber;
        Model = arg.Model;
        Title = arg.Title;
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
        Code = arg.Code;
        VersionNumber = arg.VersionNumber;
        if (arg.DataCenterId.HasValue) DataCenterId = new(arg.DataCenterId.Value);
        if (arg.OperationalStatusId.HasValue) OperationalStatusId = new(arg.OperationalStatusId.Value);
    }
    #region Guards
    private static async Task CreateGuards(CreateAssetArg arg, IAssetDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();

        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyAssetArg arg, IAssetDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();

        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    
    
    public void AddAssignedStaffs(List<CreateAssetAssignedStaffArg> args)
    {
        foreach (var arg in args)
        {
            var entity = AssetAssignedStaff.Create(arg);
            _assetAssignedStaffs.Add(entity);
        }
    }
    
    
    public void AddAssetService(List<CreateServiceAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ServiceAsset.Create(arg);
            _serviceAssets.Add(entity);
        }
    }
    
    public void AddAssetDocument(List<CreateAssetDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = AssetDocument.Create(arg);
            _assetDocuments.Add(entity);
        }
    }
    
    public void AddConfigurationItemAsset(List<CreateConfigurationItemAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ConfigurationItemAsset.Create(arg);
            _configurationItemAssets.Add(entity);
        }
    }

    public void AddComplexAssetAsset(List<CreateComplexAssetArg> args)
    {
        foreach (var arg in args)
        {
            var entity = ComplexAsset.Create(arg);
            _assets.Add(entity);
        }
    }

    public void AddAssetCustomFieldValueAsset(List<CreateAssetCustomFieldValueArg> args)
    {
        foreach (var arg in args)
        {
            var entity = Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities.AssetCustomFieldValue.Create(arg);
            _assetCustomFieldValue.Add(entity);
        }
    }

    
    public AssetId Id { get; private set; }
    public string? SerialNumber { get; private set; }
    public string? Code { get; private set; }
    public SupplierId? SupplierId { get; private set; }
    public virtual Supplier? Supplier { get; private set; }
    public StaffId? OwnerId { get; private set; }
    public virtual Staff? Owner { get; private set; }
    public AssetTypeId AssetTypeId { get; private set; }
    public virtual AssetType AssetType { get; private set; }
    public CategoryId? AssetCategoryId { get; private set; }
    public virtual Category? AssetCategory { get; private set; }
    public WarehouseId? WarehouseId { get; private set; }
    public virtual Warehouse? Warehouse { get; private set; }
    public DataCenterId? DataCenterId { get; private set; }
    public virtual DataCenter? DataCenter { get; private set; }
    public string? Model { get; private set; }
    public string? Title { get; private set; }
    public string? VersionNumber { get; private set; }
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
    public OperationalStatusId? OperationalStatusId { get; private set; }
    public virtual OperationalStatus? OperationalStatus { get; private set; }
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
        DeleteAssetIssues(userId);
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
    private List<EffectedAsset> _effectedAssets = new();
    public ICollection<EffectedAsset> EffectedAssets => _effectedAssets;

    private List<ComplexAsset> _assets = new();
    public ICollection<ComplexAsset> Assets => _assets;

    private List<ComplexAsset> _parentAssets = new();
    public ICollection<ComplexAsset> ParentAssets => _parentAssets;
    
    private List<AssetCustomFieldValue> _assetCustomFieldValue = new();
    public ICollection<AssetCustomFieldValue> AssetCustomFieldValue => _assetCustomFieldValue;   
            
    private List<AssetAssignedStaff> _assetAssignedStaffs = new();
    public ICollection<AssetAssignedStaff> AssetAssignedStaffs => _assetAssignedStaffs;

    private List<ConfigurationItemCustomFieldValue> _configurationItemCustomFieldValues = new();
    public ICollection<ConfigurationItemCustomFieldValue> ConfigurationItemCustomFieldValues => _configurationItemCustomFieldValues;
    
    
    
    
    public void DeleteAssetIssues(long userId)
    {
        foreach (var item in _assetIssues)
        {
            AddDomainEvent(new DeleteAssetEvent(issueId: item.IssueId.Value));
            item.Delete(userId);
        }
    }
    
    
    public void AddAssetIssues(List<CreateAssetIssueArg> args)
    {
        foreach (var arg in args)
        {
            var entity = AssetIssue.Create(arg);
            _assetIssues.Add(entity);
        }
    }
    
    
    
    
    public void ModifyCreateAssetCustomFieldValues(List<CreateAssetCustomFieldValueArg> args)
    {
        var activeEntities = _assetCustomFieldValue.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.AssetCustomFieldId == x.AssetCustomFieldId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.AssetCustomFieldId.Value == x.AssetCustomFieldId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _assetCustomFieldValue.FirstOrDefault(x => x.AssetCustomFieldId.Value == arg.AssetCustomFieldId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities.AssetCustomFieldValue.Create(arg);
                _assetCustomFieldValue.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }

    
    public void ModifyServiceAssets(List<CreateServiceAssetArg> args)
    {
        var activeEntities = _serviceAssets.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ServiceId == x.ServiceId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ServiceId.Value == x.ServiceId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _serviceAssets.FirstOrDefault(x => x.ServiceId.Value == arg.ServiceId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ServiceAsset.Create(arg);
                _serviceAssets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    
    
    public void ModifyAssignedStaffs(List<CreateAssetAssignedStaffArg> args)
    {
        var activeEntities = _assetAssignedStaffs.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.StaffId == x.StaffId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.StaffId.Value == x.StaffId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _assetAssignedStaffs.FirstOrDefault(x => x.StaffId.Value == arg.StaffId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = AssetAssignedStaff.Create(arg);
                _assetAssignedStaffs.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    
    public void ModifyAssetDocuments(List<CreateAssetDocumentArg> args)
    {
        var activeEntities = _assetDocuments.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _assetDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = AssetDocument.Create(arg);
                _assetDocuments.Add(entity);
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
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ConfigurationItemId == x.ConfigurationItemId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ConfigurationItemId.Value == x.ConfigurationItemId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _configurationItemAssets.FirstOrDefault(x => x.ConfigurationItemId.Value == arg.ConfigurationItemId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
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
    
    public void ModifyComplexAssets(List<CreateComplexAssetArg> args)
    {
        var activeEntities = _assets.Where(x => x.ActiveStatusId != (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.ParentAssetId == x.ParentAssetId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.ParentAssetId.Value == x.ParentAssetId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _assets.FirstOrDefault(x => x.ParentAssetId.Value == arg.ParentAssetId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = ComplexAsset.Create(arg);
                _assets.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }

    
}

