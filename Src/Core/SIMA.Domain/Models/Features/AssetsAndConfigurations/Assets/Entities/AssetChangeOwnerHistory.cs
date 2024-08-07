using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Args;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;

public class AssetChangeOwnerHistory : Entity
{
    private AssetChangeOwnerHistory() { }
    private AssetChangeOwnerHistory(CreateAssetChangeOwnerHistoryArg arg)
    {
        Id = new(arg.Id);
        AssetId = new(arg.AssetId);
        if (arg.FromOwnerId.HasValue) FromOwnerId = new(arg.FromOwnerId.Value);
        if (arg.ToOwnerId.HasValue) ToOwnerId = new(arg.ToOwnerId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public AssetChangeOwnerHistoryId Id { get; private set; }
    public AssetId AssetId { get; private set; }
    public virtual Asset Asset { get; private set; }
    public StaffId? FromOwnerId { get; private set; }
    public virtual Staff? FromOwner { get; private set; }
    public StaffId? ToOwnerId { get; private set; }
    public virtual Staff? ToOwner { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}

