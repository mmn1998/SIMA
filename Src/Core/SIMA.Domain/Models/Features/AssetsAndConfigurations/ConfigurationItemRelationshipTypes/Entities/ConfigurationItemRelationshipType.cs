using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;

public class ConfigurationItemRelationshipType : Entity, IAggregateRoot
{
    private ConfigurationItemRelationshipType() { }
    private ConfigurationItemRelationshipType(CreateConfigurationItemRelationshipTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationItemRelationshipType> Create(CreateConfigurationItemRelationshipTypeArg arg, IConfigurationItemRelationshipTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConfigurationItemRelationshipType(arg);
    }
    public async Task Modify(ModifyConfigurationItemRelationshipTypeArg arg, IConfigurationItemRelationshipTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemRelationshipTypeArg arg, IConfigurationItemRelationshipTypeDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyConfigurationItemRelationshipTypeArg arg, IConfigurationItemRelationshipTypeDomainService service)
    {

    }
    #endregion
    public ConfigurationItemRelationshipTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
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
    private List<ConfigurationItemRelationship> _configurationItemRelationships = new();
    public ICollection<ConfigurationItemRelationship> ConfigurationItemRelationships => _configurationItemRelationships;
}
