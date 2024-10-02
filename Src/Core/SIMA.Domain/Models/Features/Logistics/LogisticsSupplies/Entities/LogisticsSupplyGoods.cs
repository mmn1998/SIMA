using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class LogisticsSupplyGoods : Entity
{
    public LogisticsSupplyGoodsId Id { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public LogisticsRequestGoodsId LogisticsRequestGoodsId { get; private set; }
    public virtual LogisticsRequestGoods LogisticsRequestGoods { get; private set; }
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
    private List<GoodsCoding> _goodsCodings = new();
    public ICollection<GoodsCoding> GoodsCodings => _goodsCodings;
}
