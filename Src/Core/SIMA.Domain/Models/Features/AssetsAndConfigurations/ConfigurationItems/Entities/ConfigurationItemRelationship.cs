using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemRelationship : Entity
{
    private ConfigurationItemRelationship() { }
    private ConfigurationItemRelationship(CreateConfigurationItemRelationshipArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemVersioningId = new(arg.ConfigurationItemVersioningId);
        ConfigurationItemRelationshipTypeId = new(arg.ConfigurationItemRelationshipTypeId);
        RelatedConfigurationItemVersioningId = new(arg.RelatedConfigurationItemVersioningId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemRelationship Create(CreateConfigurationItemRelationshipArg arg)
    {
        return new ConfigurationItemRelationship(arg);
    }
    public ConfigurationItemRelationshipId Id { get; private set; }
    public ConfigurationItemVersioningId ConfigurationItemVersioningId { get; private set; }
    public virtual ConfigurationItemVersioning ConfigurationItemVersioning { get; private set; }
    public ConfigurationItemVersioningId RelatedConfigurationItemVersioningId { get; private set; }
    public virtual ConfigurationItemVersioning RelatedConfigurationItemVersioning { get; private set; }
    public ConfigurationItemRelationshipTypeId ConfigurationItemRelationshipTypeId { get; private set; }
    public virtual ConfigurationItemRelationshipType ConfigurationItemRelationshipType { get; private set; }
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
}

