using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;

public class AssetCustomFieldOption : Entity
{
    public AssetCustomFieldOption()
    {
    }
    private AssetCustomFieldOption(CreateAssetCustomFieldOptionArg arg)
    {
        Id = new(arg.Id);
        AssetCustomFieldId = new (arg.AssetCustomFieldId);
        OptionValue = arg.OptionValue;
        OptionText = arg.OptionText;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AssetCustomFieldOption Create(CreateAssetCustomFieldOptionArg arg)
    {
        CreateGuards(arg);
        return new AssetCustomFieldOption(arg);
    }
    public void Modify(ModifyAssetCustomFieldOptionArg arg)
    {
        ModifyGuards(arg);
        AssetCustomFieldId = new (arg.AssetCustomFieldId.Value);
        OptionValue = arg.OptionValue;
        OptionText = arg.OptionText;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    
    
    #region Guards
    private static void CreateGuards(CreateAssetCustomFieldOptionArg arg)
    {
        arg.OptionText.NullCheck();
        arg.OptionValue.NullCheck();
    }
    private void ModifyGuards(ModifyAssetCustomFieldOptionArg arg)
    {
        arg.OptionText.NullCheck();
        arg.OptionValue.NullCheck();
        
    }
    #endregion
    
    public AssetCustomFieldOptionId Id { get; private set; }
    public AssetCustomFieldId AssetCustomFieldId { get; private set; }
    public AssetCustomField AssetCustomField { get; private set; }
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