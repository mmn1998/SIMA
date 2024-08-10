using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemChangeOwnerHistory : Entity
{
    private ConfigurationItemChangeOwnerHistory() { }
    private ConfigurationItemChangeOwnerHistory(CreateConfigurationItemChangeOwnerHistoryArg arg)
    {
        Id = new(arg.Id);
        FromOwnerId = new(arg.FromOwnerId);
        ToOwnerId = new(arg.ToOwnerId);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemChangeOwnerHistory Create(CreateConfigurationItemChangeOwnerHistoryArg arg)
    {
        return new ConfigurationItemChangeOwnerHistory(arg);
    }
    public ConfigurationItemChangeOwnerHistoryId Id { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public StaffId FromOwnerId { get; private set; }
    public virtual Staff FromOwner { get; private set; }
    public StaffId ToOwnerId { get; private set; }
    public virtual Staff ToOwner { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

