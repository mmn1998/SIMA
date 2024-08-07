using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.ValueObjects;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;

public class AssetPhysicalStatus : Entity, IAggregateRoot
{
    private AssetPhysicalStatus() { }
    private AssetPhysicalStatus(CreateAssetPhysicalStatusArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<AssetPhysicalStatus> Create(CreateAssetPhysicalStatusArg arg, IAssetPhysicalStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new AssetPhysicalStatus(arg);
    }
    public async Task Modify(ModifyAssetPhysicalStatusArg arg, IAssetPhysicalStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateAssetPhysicalStatusArg arg, IAssetPhysicalStatusDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyAssetPhysicalStatusArg arg, IAssetPhysicalStatusDomainService service)
    {

    }
    #endregion
    public AssetPhysicalStatusId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
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
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
    private List<AssetChangePhysicalStatusHistory> _fromAssetChangePhysicalStatusHistories = new();
    public ICollection<AssetChangePhysicalStatusHistory> FromAssetChangePhysicalStatusHistories => _fromAssetChangePhysicalStatusHistories;
    private List<AssetChangePhysicalStatusHistory> _toAssetChangePhysicalStatusHistories = new();
    public ICollection<AssetChangePhysicalStatusHistory> ToAssetChangePhysicalStatusHistories => _toAssetChangePhysicalStatusHistories;
}
