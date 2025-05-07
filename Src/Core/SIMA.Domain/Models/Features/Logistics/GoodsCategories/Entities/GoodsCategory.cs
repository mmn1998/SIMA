using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;

public class GoodsCategory : Entity, IAggregateRoot
{
    private GoodsCategory() { }
    private GoodsCategory(CreateGoodsCategoryArg arg)
    {
        Id = new(arg.Id);
        GoodsTypeId = new(arg.GoodsTypeId);
        Name = arg.Name;
        Code = arg.Code;
        IsGoods = arg.IsGoods;
        IsHardware = arg.IsHardware;
        IsRequiredSecurityCheck = arg.IsRequiredSecurityCheck;
        IsFixedAsset = arg.IsFixedAsset;
        IsTechnological = arg.IsTechnological;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async static Task<GoodsCategory> Create(CreateGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        await CreateGuards(arg, service);
        return new GoodsCategory(arg);
    }
    public async Task Modify(ModifyGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        await ModifyGuards(arg, service);
        GoodsTypeId = new(arg.GoodsTypeId);
        Name = arg.Name;
        Code = arg.Code;
        IsGoods = arg.IsGoods;
        IsHardware = arg.IsHardware;
        IsRequiredSecurityCheck = arg.IsRequiredSecurityCheck;
        IsFixedAsset = arg.IsFixedAsset;
        IsTechnological = arg.IsTechnological;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    #region Guards
    private static async Task CreateGuards(CreateGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyGoodsCategoryArg arg, IGoodsCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion

    public GoodsCategoryId Id { get; private set; }
    public GoodsTypeId GoodsTypeId { get; private set; }
    public virtual GoodsType GoodsType { get; private set; }
    public string IsTechnological { get; private set; }
    public string IsHardware { get; private set; }
    public string IsGoods { get; private set; }
    public string IsFixedAsset { get; private set; }
    public string IsRequiredSecurityCheck { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long?CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        DeleteGoodsCategorySuppliers(userId);
    }
    public void AddGoodsCategorySuppliers(List<CreateGoodsCategorySupplierArg> args)
    {
        foreach (var arg in args)
        {
            var entity = GoodsCategorySupplier.Create(arg);
            _goodsCategorySuppliers.Add(entity);
        }
    }
    public void DeleteGoodsCategorySuppliers(long userId)
    {
        foreach(var entity in _goodsCategorySuppliers)
        {
            entity.Delete(userId);
        }
    }
    public void ModifyGoodsCategorySuppliers(List<CreateGoodsCategorySupplierArg> args)
    {
        var activeEntities = _goodsCategorySuppliers.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.SupplierId == x.SupplierId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.SupplierId.Value == x.SupplierId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _goodsCategorySuppliers.FirstOrDefault(x => x.SupplierId.Value == arg.SupplierId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy);
            }
            else
            {
                entity = GoodsCategorySupplier.Create(arg);
                _goodsCategorySuppliers.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy);
        }
    }
    private List<Goods> _goods = new();
    public ICollection<Goods> Goods => _goods;

    private List<LogisticsRequestGoods> _logisticsRequestGoods = new();
    public ICollection<LogisticsRequestGoods> LogisticsRequestGoods => _logisticsRequestGoods;

    private List<GoodsCategorySupplier> _goodsCategorySuppliers = new();
    public ICollection<GoodsCategorySupplier> GoodsCategorySuppliers => _goodsCategorySuppliers;
}
