using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemChangeStatusHistory : Entity
{
    private ConfigurationItemChangeStatusHistory() { }
    private ConfigurationItemChangeStatusHistory(CreateConfigurationItemChangeStatusHistoryArg arg)
    {
        Id = new(arg.Id);
        ToConfigurationItemStatusId = new(arg.ToConfigurationItemStatusId);
        FromConfigurationItemStatusId = new(arg.FromConfigurationItemStatusId);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemChangeStatusHistory Create(CreateConfigurationItemChangeStatusHistoryArg arg)
    {
        return new ConfigurationItemChangeStatusHistory(arg);
    }
    public ConfigurationItemChangeStatusHistoryId Id { get; private set; }
    public ConfigurationItemStatusId FromConfigurationItemStatusId { get; private set; }
    public virtual ConfigurationItemStatus FromConfigurationItemStatus { get; private set; }
    public ConfigurationItemStatusId ToConfigurationItemStatusId { get; private set; }
    public virtual ConfigurationItemStatus ToConfigurationItemStatus { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

