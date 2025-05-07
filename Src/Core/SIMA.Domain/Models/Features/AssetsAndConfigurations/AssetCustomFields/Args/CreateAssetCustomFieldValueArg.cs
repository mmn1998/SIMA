namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetCustomFields.Args;

public class CreateAssetCustomFieldValueArg
{
    public long Id { get; set; }
    public long AssetCustomFieldId { get; set; }
    public long AssetId { get; set; }
    public string ItemValue { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}