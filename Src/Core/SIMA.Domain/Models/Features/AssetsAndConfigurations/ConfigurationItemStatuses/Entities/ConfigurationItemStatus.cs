using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Contracts;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;

public class ConfigurationItemStatus : Entity
{
    private ConfigurationItemStatus() { }
    private ConfigurationItemStatus(CreateConfigurationItemStatusArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationItemStatus> Create(CreateConfigurationItemStatusArg arg, IConfigurationItemStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConfigurationItemStatus(arg);
    }
    public async Task Modify(ModifyConfigurationItemStatusArg arg, IConfigurationItemStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemStatusArg arg, IConfigurationItemStatusDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyConfigurationItemStatusArg arg, IConfigurationItemStatusDomainService service)
    {

    }
    #endregion
    public ConfigurationItemStatusId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
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
    private List<ConfigurationItemChangeStatusHistory> _fromConfigurationItemChangeStatusHistories = new();
    public ICollection<ConfigurationItemChangeStatusHistory> FromConfigurationItemChangeStatusHistories => _fromConfigurationItemChangeStatusHistories;
    private List<ConfigurationItemChangeStatusHistory> _toConfigurationItemChangeStatusHistories = new();
    public ICollection<ConfigurationItemChangeStatusHistory> ToConfigurationItemChangeStatusHistories => _toConfigurationItemChangeStatusHistories;
    private List<ConfigurationItem> _configurationItems = new();
    public ICollection<ConfigurationItem> ConfigurationItems => _configurationItems;
}
