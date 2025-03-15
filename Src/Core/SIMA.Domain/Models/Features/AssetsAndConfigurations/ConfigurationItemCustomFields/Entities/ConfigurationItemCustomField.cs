using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomeFieldTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemCustomFields.Entities;

public class ConfigurationItemCustomField : Entity, IAggregateRoot
{
    private ConfigurationItemCustomField()
    {
        
    }
    private ConfigurationItemCustomField(CreateConfigurationItemCustomFieldArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        DisplayName = arg.DisplayName;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ConfigurationItemTypeId = new(arg.ConfigurationItemTypeId);
        CustomeFieldTypeId = new(arg.ConfigurationItemTypeId);
        IsMandetory = arg.IsMandetory;
        BoundingViewName = arg.BoundingViewName;
        ValueBoundingFeild = arg.ValueBoundingFeild;
        TextBoundingFeild = arg.TextBoundingFeild;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ConfigurationItemCustomField Create(CreateConfigurationItemCustomFieldArg arg)
    {
        CreateGuards(arg);
        return new ConfigurationItemCustomField(arg);
    }
    public void Modify(ModifyConfigurationItemCustomFieldArg arg)
    {
        ModifyGuards(arg);
        Name = arg.Name;
        DisplayName = arg.DisplayName;
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        ConfigurationItemTypeId = new(arg.ConfigurationItemTypeId);
        CustomeFieldTypeId = new(arg.ConfigurationItemTypeId);
        IsMandetory = arg.IsMandetory;
        BoundingViewName = arg.BoundingViewName;
        ValueBoundingFeild = arg.ValueBoundingFeild;
        TextBoundingFeild = arg.TextBoundingFeild;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateConfigurationItemCustomFieldArg arg)
    {
        arg.Name.NullCheck();
        arg.DisplayName.NullCheck();
        arg.IsMandetory.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.DisplayName.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.TextBoundingFeild) && arg.TextBoundingFeild.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.BoundingViewName) && arg.BoundingViewName.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.ValueBoundingFeild) && arg.ValueBoundingFeild.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    private void ModifyGuards(ModifyConfigurationItemCustomFieldArg arg)
    {
        arg.Name.NullCheck();
        arg.DisplayName.NullCheck();
        arg.IsMandetory.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.DisplayName.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.TextBoundingFeild) && arg.TextBoundingFeild.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.BoundingViewName) && arg.BoundingViewName.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!string.IsNullOrEmpty(arg.ValueBoundingFeild) && arg.ValueBoundingFeild.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public ConfigurationItemCustomFieldId Id { get; private set; }
    public ConfigurationItemCustomFieldId? ParentId { get; private set; }
    public virtual ConfigurationItemCustomField? Parent { get; private set; }
    public ConfigurationItemTypeId ConfigurationItemTypeId { get; private set; }
    public virtual ConfigurationItemType ConfigurationItemType { get; private set; }
    public CustomeFieldTypeId CustomeFieldTypeId { get; private set; }
    public virtual CustomeFieldType CustomeFieldType { get; private set; }
    public string? Name { get; private set; }
    public string? IsMandetory { get; private set; }
    public string? DisplayName { get; private set; }
    public string? BoundingViewName { get; private set; }
    public string? ValueBoundingFeild { get; private set; }
    public string? TextBoundingFeild { get; private set; }
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