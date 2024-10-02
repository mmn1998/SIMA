using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityConfigurationItem
{
    private CriticalActivityConfigurationItem() { }
    private CriticalActivityConfigurationItem(CreateCriticalActivityConfigurationItemArg arg)
    {
        Id = new(arg.Id);
        CriticalActivityId = new(arg.CriticalActivityId);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static CriticalActivityConfigurationItem Create(CreateCriticalActivityConfigurationItemArg arg)
    {
        return new CriticalActivityConfigurationItem(arg);
    }

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

    public CriticalActivityConfigurationItemId Id { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
