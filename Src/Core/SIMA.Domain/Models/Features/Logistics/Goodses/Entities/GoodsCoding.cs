using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.Goodses.Entities;

public class GoodsCoding : Entity
{
    public GoodsCodingId Id { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public GoodsId GoodsId { get; private set; }
    public virtual Goods Goods { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}