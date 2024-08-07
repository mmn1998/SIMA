namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetDocumentArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}

