using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Args;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Contracts;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;

public class AssetTechnicalStatus : Entity, IAggregateRoot
{
    private AssetTechnicalStatus() { }
    private AssetTechnicalStatus(CreateAssetTechnicalStatusArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<AssetTechnicalStatus> Create(CreateAssetTechnicalStatusArg arg, IAssetTechnicalStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new AssetTechnicalStatus(arg);
    }
    public async Task Modify(ModifyAssetTechnicalStatusArg arg, IAssetTechnicalStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateAssetTechnicalStatusArg arg, IAssetTechnicalStatusDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyAssetTechnicalStatusArg arg, IAssetTechnicalStatusDomainService service)
    {

    }
    #endregion
    public AssetTechnicalStatusId Id { get; private set; }
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
    private List<AssetChangeTechnicalStatusHistory> _fromAssetChangeTechnicalStatusHistories = new();
    public ICollection<AssetChangeTechnicalStatusHistory> FromAssetChangeTechnicalStatusHistories => _fromAssetChangeTechnicalStatusHistories;
    private List<AssetChangeTechnicalStatusHistory> _toAssetChangeTechnicalStatusHistories = new();
    public ICollection<AssetChangeTechnicalStatusHistory> ToAssetChangeTechnicalStatusHistories => _toAssetChangeTechnicalStatusHistories;
}

