using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;

public class ConfigurationItemDataProcedure : Entity
{

    private ConfigurationItemDataProcedure() { }
    private ConfigurationItemDataProcedure(CreateConfigurationItemDataProcedureArg arg)
    {
        Id = new(arg.Id);
        DataProcedureId  =new (arg.DataProcedureId);
        ConfigurationItemId =  new (arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConfigurationItemDataProcedure> Create(CreateConfigurationItemDataProcedureArg arg, IConfigurationItemDataProcedureDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConfigurationItemDataProcedure(arg);
    }
    public async Task Modify(ModifyConfigurationItemDataProcedureArg arg, IConfigurationItemDataProcedureDomainService service)
    {
        await ModifyGuards(arg, service);
        DataProcedureId  =new (arg.DataProcedureId);
        ConfigurationItemId =  new (arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    
    
    
    #region Guards
    private static async Task CreateGuards(CreateConfigurationItemDataProcedureArg arg, IConfigurationItemDataProcedureDomainService service)
    {
    }
    private async Task ModifyGuards(ModifyConfigurationItemDataProcedureArg arg, IConfigurationItemDataProcedureDomainService service)
    {

    }
    #endregion
    public ConfigurationItemDataProcedureId Id { get; private set; }
    public DataProcedureId DataProcedureId { get; private set; }
    public DataProcedure DataProcedure { get; private set; }
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
}