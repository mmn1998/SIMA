using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;

public class ConfigurationItemCustomFieldOption : Entity
{
    public ConfigurationItemCustomFieldOption()
    {
        
    }
    
    private ConfigurationItemCustomFieldOption(CreateConfigurationItemCustomFieldOptionArg arg)
    {
        Id = new(arg.Id);
        ConfigurationItemCustomFieldId = new (arg.ConfigurationItemCustomFieldId.Value);
        OptionValue = arg.OptionValue;
        OptionText = arg.OptionText;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    
    
    public static ConfigurationItemCustomFieldOption Create(CreateConfigurationItemCustomFieldOptionArg arg)
    {
        CreateGuards(arg);
        return new ConfigurationItemCustomFieldOption(arg);
    }
    public void Modify(ModifyConfigurationItemCustomFieldOptionArg arg)
    {
        ModifyGuards(arg);
        ConfigurationItemCustomFieldId = new (arg.ConfigurationItemCustomFieldId.Value);
        OptionValue = arg.OptionValue;
        OptionText = arg.OptionText;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateConfigurationItemCustomFieldOptionArg arg)
    {
        arg.OptionValue.NullCheck();
        arg.OptionText.NullCheck();
        if (arg.OptionText.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.OptionValue.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private void ModifyGuards(ModifyConfigurationItemCustomFieldOptionArg arg)
    {
        arg.OptionValue.NullCheck();
        arg.OptionText.NullCheck();
        if (arg.OptionText.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.OptionValue.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public ConfigurationItemCustomFieldOptionId Id { get; private set; }
    public ConfigurationItemCustomFieldId ConfigurationItemCustomFieldId { get; private set; }
    public virtual ConfigurationItemCustomField ConfigurationItemCustomField { get; private set; }
    public string OptionValue { get; private set; }
    public string OptionText { get; private set; }
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