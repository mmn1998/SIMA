using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.DataProcedures.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

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
    public static ConfigurationItemDataProcedure Create(CreateConfigurationItemDataProcedureArg arg)
    {
        return new ConfigurationItemDataProcedure(arg);
    }
    public void Modify(ModifyConfigurationItemDataProcedureArg arg)
    {
        DataProcedureId  =new (arg.DataProcedureId);
        ConfigurationItemId =  new (arg.ConfigurationItemId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}