namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class CreateAssetCustomFieldArg
{
    public long Id { get; set; }
    public long? ParentId { get; set; }
    public long AssetTypeId { get; set; }
    public long CustomeFieldTypeId { get; set; }
    public string? Name { get; set; }
    public string? IsMandetory { get; set; }
    public string? DisplayName { get; set; }
    public string? BoundingViewName { get; set; }
    public string? ValueBoundingFeild { get; set; }
    public string? TextBoundingFeild { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}