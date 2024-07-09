using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.Args;
using SIMA.Domain.Models.Features.Logistics.Goodses.Contracts;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequestGoodss.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.Goodses.Entities;

public class Goods : Entity, IAggregateRoot
{
    private Goods() { }
    private Goods(CreateGoodsArg arg)
    {
        Id = new(arg.Id);
        GoodsCategoryId = new(arg.GoodsCategoryId);
        UnitMeasurementId = new(arg.UnitMeasurementId);
        Name = arg.Name;
        Code = arg.Code;
        IsFixedAsset = arg.IsFixedAsset;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Goods> Create(CreateGoodsArg arg, IGoodsDomainService service)
    {
        await CreateGuards(arg, service);
        return new Goods(arg);
    }
    public async Task Modify(ModifyGoodsArg arg, IGoodsDomainService service)
    {
        await ModifyGuards(arg, service);
        GoodsCategoryId = new(arg.GoodsCategoryId);
        UnitMeasurementId = new(arg.UnitMeasurementId);
        Name = arg.Name;
        Code = arg.Code;
        IsFixedAsset = arg.IsFixedAsset;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateGoodsArg arg, IGoodsDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyGoodsArg arg, IGoodsDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public GoodsId Id { get; private set; }
    public GoodsCategoryId GoodsCategoryId { get; private set; }
    public virtual GoodsCategory GoodsCategory { get; private set; }
    public UnitMeasurementId UnitMeasurementId { get; private set; }
    public virtual UnitMeasurement UnitMeasurement { get; private set; }
    public string? IsFixedAsset { get; private set; }
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
    private List<LogisticsRequestGoods> _logisticsRequestGoods = new();
    public ICollection<LogisticsRequestGoods> LogisticsRequestGoods => _logisticsRequestGoods;
    private List<GoodsCoding> _goodsCodings = new();
    public ICollection<GoodsCoding> GoodsCodings => _goodsCodings;
}
