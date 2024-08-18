using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
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
        CompanyBuildingLocationId = new(arg.CompanyBuildingLocationId);
        Code = arg.Code;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
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
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemArg arg, IConfigurationItemDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyConfigurationItemArg arg, IConfigurationItemDomainService service)
    {

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
    public SupplierId? LicenseSupplierId { get; private set; }
    public virtual Supplier? LicenseSupplier { get; private set; }
    public LocationId CompanyBuildingLocationId { get; private set; }
    public virtual Location CompanyBuildingLocation { get; private set; }
    public string? Code { get; private set; }
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
}
