using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Args;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;

public class ConfigurationAttributeValue
{
    private ConfigurationAttributeValue() { }
    private ConfigurationAttributeValue(CreteConfigurationAttribiteValueArg arg)
    {
        Id = new ConfigurationAttributeValueId(IdHelper.GenerateUniqueId());
        ConfigurationAttributeId = new ConfigurationAttributeId(arg.ConfigurationAttributeId);
        Value = arg.Value;
        IsUserConfige = arg.IsUserConfige;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public static async Task<ConfigurationAttributeValue> Create(CreteConfigurationAttribiteValueArg arg)
    {
        return new ConfigurationAttributeValue(arg);
    }
    public ConfigurationAttributeValueId Id { get; private set; }

    public ConfigurationAttributeId ConfigurationAttributeId { get; private set; }

    public string? Value { get; private set; }

    public string? IsUserConfige { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual ConfigurationAttribute ConfigurationAttribute { get; private set; } = null!;
}
