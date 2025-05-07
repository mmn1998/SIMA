using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemAccessInfo : Entity
{
    private ConfigurationItemAccessInfo() { }
    private ConfigurationItemAccessInfo(CreateConfigurationItemAccessInfoArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        IPAddressFrom = arg.IPAddressFrom;
        IPAddressTo = arg.IPAddressTo;
        PortFrom = arg.PortFrom;
        PortTo = arg.PortTo;
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
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public string? IPAddressFrom { get; private set; }
    public string? IPAddressTo { get; private set; }
    public string? PortFrom { get; private set; }
    public string? PortTo { get; private set; }
    public DateTime ActiveFrom { get; private set; }
    public DateTime? ActiveTo { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
