using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemAssetHistory : Entity
{
    private ConfigurationItemAssetHistory() { }
    private ConfigurationItemAssetHistory(CreateConfigurationItemAssetHistoryArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        ConfigurationItemVersioningId = new(arg.ConfigurationItemVersioningId);
        IsAssigned = arg.IsAssigned;
        AssignDate = arg.AssignDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemAssetHistory Create(CreateConfigurationItemAssetHistoryArg arg)
    {
        return new ConfigurationItemAssetHistory(arg);
    }
    public ConfigurationItemAssetHistoryId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public string? IsAssigned { get; private set; }
    public DateOnly? AssignDate { get; private set; }
    public ConfigurationItemVersioningId ConfigurationItemVersioningId { get; private set; }
    public virtual ConfigurationItemVersioning ConfigurationItemVersioning { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

