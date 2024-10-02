using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Entities;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Warehouses.Args;
using SIMA.Domain.Models.Features.Auths.Warehouses.Contracts;
using SIMA.Domain.Models.Features.Auths.Warehouses.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Warehouses.Entities;

public class Warehouse : Entity, IAggregateRoot
{
    private Warehouse()
    {

    }
    private Warehouse(CreateWarehouseArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Warehouse> Create(CreateWarehouseArg arg, IWarehouseDomainService service)
    {
        await CreateGuards(arg, service);
        return new Warehouse(arg);
    }
    public async Task Modify(ModifyWarehouseArg arg, IWarehouseDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateWarehouseArg arg, IWarehouseDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyWarehouseArg arg, IWarehouseDomainService service)
    {

    }
    #endregion
    public WarehouseId Id { get; private set; }
    public CompanyBuildingLocationId CompanyBuildingLocationId { get; private set; }
    public virtual CompanyBuildingLocation CompanyBuildingLocation { get; private set; }
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
    private List<AssetWarehouseHistory> _assetWarehouseHistories = new();
    public ICollection<AssetWarehouseHistory> AssetWarehouseHistories => _assetWarehouseHistories;
}