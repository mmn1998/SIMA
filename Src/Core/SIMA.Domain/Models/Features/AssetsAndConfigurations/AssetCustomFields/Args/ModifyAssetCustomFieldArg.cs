namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class ModifyAssetCustomFieldArg
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long AssetTypeId { get; set; }
    public long CustomFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? IsMandatory { get; set; }
    public string? DisplayName { get; set; }
    public string? BoundingViewName { get; set; }
    public string? ValueBoundingFeild { get; set; }
    public string? TextBoundingFeild { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
