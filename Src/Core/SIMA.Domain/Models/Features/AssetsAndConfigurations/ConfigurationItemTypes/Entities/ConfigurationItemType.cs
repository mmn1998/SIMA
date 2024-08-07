using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Contracts;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;

public class ConfigurationItemType : Entity
{
    private ConfigurationItemType() { }
    private ConfigurationItemType(CreateConfigurationItemTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationItemType> Create(CreateConfigurationItemTypeArg arg, IConfigurationItemTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConfigurationItemType(arg);
    }
    public async Task Modify(ModifyConfigurationItemTypeArg arg, IConfigurationItemTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemTypeArg arg, IConfigurationItemTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyConfigurationItemTypeArg arg, IConfigurationItemTypeDomainService service)
    {

    }
    #endregion
    public ConfigurationItemTypeId Id { get; private set; }
    public ConfigurationItemTypeId? ParentId { get; private set; }
    public virtual ConfigurationItemType? Parent { get; private set; }
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
    private List<ConfigurationItem> _configurationItems = new();
    public ICollection<ConfigurationItem> ConfigurationItems => _configurationItems;
}
