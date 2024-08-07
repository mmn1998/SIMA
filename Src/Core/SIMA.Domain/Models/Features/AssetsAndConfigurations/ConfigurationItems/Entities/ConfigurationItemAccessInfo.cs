using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemAccessInfo : Entity
{
    private ConfigurationItemAccessInfo() { }
    private ConfigurationItemAccessInfo(CreateConfigurationItemAccessInfoArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemVersioningId = new(arg.ConfigurationItemVersioningId);
        IPAddress = arg.IPAddress;
        Port = arg.Port;
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemAccessInfo Create(CreateConfigurationItemAccessInfoArg arg)
    {
        return new ConfigurationItemAccessInfo(arg);
    }
    public ConfigurationItemAccessInfoId Id { get; private set; }
    public ConfigurationItemVersioningId ConfigurationItemVersioningId { get; private set; }
    public virtual ConfigurationItemVersioning ConfigurationItemVersioning { get; private set; }
    public string? IPAddress { get; private set; }
    public string? Port { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

