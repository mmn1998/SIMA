namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;

public class CreateAssetIssueArg
{
    public long Id { get; set; }
    public long AssetId { get; set; }
    public long IssueId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
