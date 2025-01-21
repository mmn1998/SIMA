using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class LogisticsSupplyGoods : Entity
{
    private LogisticsSupplyGoods()
    {
        
    }
    private LogisticsSupplyGoods(CreateLogisticsSupplyGoodsArg arg)
    {
        Id = new(arg.Id);
        LogisticsRequestGoodsId = new(arg.LogisticsRequestGoodsId);
        LogisticsSupplyId = new(arg.LogisticsSupplyId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static LogisticsSupplyGoods Create(CreateLogisticsSupplyGoodsArg arg)
    {
        return new LogisticsSupplyGoods(arg);
    }
    public LogisticsSupplyGoodsId Id { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public LogisticsRequestGoodsId LogisticsRequestGoodsId { get; private set; }
    public virtual LogisticsRequestGoods LogisticsRequestGoods { get; private set; }

    public string? IsContractRequired { get; private set; }
    public string? IsPrePaymentRequired { get; private set; }
    public int? PrePaymentPercentage { get; private set; }
    public string? Description { get; private set; }
    public decimal? EstimatedPrice { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    private List<GoodsCoding> _goodsCodings = new();
    public ICollection<GoodsCoding> GoodsCodings => _goodsCodings;

    private List<OrderingItem> _orderingItems = new();
    public ICollection<OrderingItem> OrderingItems => _orderingItems;
}