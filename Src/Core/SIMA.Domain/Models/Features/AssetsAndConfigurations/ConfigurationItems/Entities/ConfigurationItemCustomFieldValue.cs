using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemCustomFieldValue : Entity
{
    private ConfigurationItemCustomFieldValue() { }
    private ConfigurationItemCustomFieldValue(CreateConfigurationItemCustomFieldValueArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        AssetId = new(arg.AssetId);
        ItemValue = arg.ItemValue;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemCustomFieldValue Create(CreateConfigurationItemCustomFieldValueArg arg)
    {
        return new ConfigurationItemCustomFieldValue(arg);
    }
    public ConfigurationItemCustomFieldValueId Id { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public AssetId AssetId{ get; private set; }
    public virtual Asset Asset { get; private set; }
    public string ItemValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

