using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class AssetIssue : Entity
{
    private AssetIssue() { }
    private AssetIssue(CreateAssetIssueArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        IssueId = new(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static AssetIssue Create(CreateAssetIssueArg arg)
    {
        return new AssetIssue(arg);
    }
    public AssetIssueId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
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

