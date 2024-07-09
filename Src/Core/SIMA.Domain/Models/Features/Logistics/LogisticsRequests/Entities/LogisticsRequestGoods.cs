using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequestGoodss.Entities;

public class LogisticsRequestGoods : Entity
{
    private LogisticsRequestGoods() 
    {
    }
    private LogisticsRequestGoods(CreateLogisticsRequestGoodsArg arg)
    {
        Id = new LogisticsRequestGoodsId(IdHelper.GenerateUniqueId());
        LogisticsRequestId = new LogisticsRequestId(arg.LogisticsRequestId);
        GoodsId = new GoodsId(arg.GoodsId);
        Quantity = arg.Quantity;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LogisticsRequestGoods> Create(CreateLogisticsRequestGoodsArg arg)
    {
        return new LogisticsRequestGoods(arg);
    }

    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public LogisticsRequestGoodsId Id { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public GoodsId GoodsId { get; private set; }
    public virtual Goods Goods { get; private set; }
    public float Quantity { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
