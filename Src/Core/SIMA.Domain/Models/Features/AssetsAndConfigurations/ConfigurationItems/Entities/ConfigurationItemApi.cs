using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Apis.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemApi : Entity
{
    private ConfigurationItemApi() { }
    private ConfigurationItemApi(CreateConfigurationItemApiArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        ApiId = new(arg.ApiId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemApi Create(CreateConfigurationItemApiArg arg)
    {
        return new ConfigurationItemApi(arg);
    }
    public ConfigurationItemApiId Id { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
    public ApiId ApiId { get; private set; }
    public virtual Api Api { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}