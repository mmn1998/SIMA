using System.Text;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Entities;

public class AssetCustomFieldValue : Entity
{
    private AssetCustomFieldValue()
    {

    }
    private AssetCustomFieldValue(CreateAssetCustomFieldValueArg arg)
    {
        Id = new(arg.Id);
        AssetCustomFieldId = new (arg.AssetCustomFieldId);
        AssetId = new(arg.AssetId);
        ItemValue = arg.ItemValue;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    
    public static AssetCustomFieldValue Create(CreateAssetCustomFieldValueArg arg)
    {
        CreateGuards(arg);
        return new AssetCustomFieldValue(arg);
    }
    public void Modify(ModifyAssetCustomFieldValueArg arg)
    {
        ModifyGuards(arg);
        ItemValue = arg.ItemValue;
        AssetCustomFieldId = new (arg.AssetCustomFieldId);
        AssetId = new (arg.AssetId);
        ItemValue = arg.ItemValue;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    
    
    
    
    #region Guards
    private static void CreateGuards(CreateAssetCustomFieldValueArg arg)
    {

    }
    private void ModifyGuards(ModifyAssetCustomFieldValueArg arg)
    {

    }
    #endregion
    
    
    
    
    public AssetCustomFieldValueId Id { get;private set; }
    public AssetCustomFieldId AssetCustomFieldId { get; private set; }
    public virtual AssetCustomField AssetCustomField { get; private set; }
    public AssetId AssetId { get;private set; }
    public virtual Asset Asset { get; private set; }
    public string ItemValue { get;private set; }
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