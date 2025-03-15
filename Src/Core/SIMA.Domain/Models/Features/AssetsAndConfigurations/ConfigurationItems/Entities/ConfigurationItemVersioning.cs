using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemVersioning : Entity
{
    private ConfigurationItemVersioning() { }
    private ConfigurationItemVersioning(CreateConfigurationItemVersioningArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemId = new(arg.ConfigurationItemId);
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemVersioning Create(CreateConfigurationItemVersioningArg arg)
    {
        return new ConfigurationItemVersioning(arg);
    }
    public void Modify(ModifyConfigurationItemVersioningArg arg)
    {
        ConfigurationItemId = new(arg.ConfigurationItemId);
        VersionNumber = arg.VersionNumber;
        ReleaseDate = arg.ReleaseDate;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public ConfigurationItemVersioningId Id { get; private set; }
    public string? VersionNumber { get; private set; }
    public DateOnly ReleaseDate { get; private set; }
    public ConfigurationItemId ConfigurationItemId { get; private set; }
    public virtual ConfigurationItem ConfigurationItem { get; private set; }
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
    //private List<ConfigurationItemAccessInfo> _configurationItemAccessInfos;
    //public ICollection<ConfigurationItemAccessInfo> ConfigurationItemAccessInfos => _configurationItemAccessInfos;
    //private List<ConfigurationItemAsset> _configurationItemAssets;
    //public ICollection<ConfigurationItemAsset> ConfigurationItemAssets => _configurationItemAssets;
    //private List<ConfigurationItemRelationship> _configurationItemRelationships;
    //public ICollection<ConfigurationItemRelationship> ConfigurationItemRelationships => _configurationItemRelationships;
    //private List<ConfigurationItemRelationship> _relatedConfigurationItemRelationships;
    //public ICollection<ConfigurationItemRelationship> RelatedConfigurationItemRelationships => _relatedConfigurationItemRelationships;
    //private List<ConfigurationItemIssue> _configurationItemIssues;
    //public ICollection<ConfigurationItemIssue> ConfigurationItemIssues => _configurationItemIssues;
    //private List<ConfigurationItemDocument> _configurationItemDocuments;
    //public ICollection<ConfigurationItemDocument> ConfigurationItemDocuments => _configurationItemDocuments;
    //private List<ConfigurationItemAssetHistory> _configurationItemAssetHistories;
    //public ICollection<ConfigurationItemAssetHistory> ConfigurationItemAssetHistories => _configurationItemAssetHistories;
}

