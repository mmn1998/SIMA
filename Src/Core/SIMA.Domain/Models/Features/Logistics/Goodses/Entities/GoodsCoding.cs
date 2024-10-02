using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.Goodses.Entities;

public class GoodsCoding : Entity
{
    public GoodsCodingId Id { get; private set; }
    public LogisticsSupplyGoodsId LogisticsSupplyGoodsId { get; private set; }
    public virtual LogisticsSupplyGoods LogisticsSupplyGoods { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}