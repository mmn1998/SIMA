using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class LogisticsRequestGoods : Entity
{
    private LogisticsRequestGoods()
    {
    }
    private LogisticsRequestGoods(CreateLogisticsRequestGoodsArg arg)
    {
        Id = new LogisticsRequestGoodsId(IdHelper.GenerateUniqueId());
        LogisticsRequestId = new LogisticsRequestId(arg.LogisticsRequestId);
        GoodsStatusId = arg.GoodsStatusId !=0 ? new GoodsStatusId(arg.GoodsStatusId) :null ;
        GoodsCategoryId = new GoodsCategoryId(arg.GoodsCategoryId);
        GoodsId = arg.GoodsId != 0 ? new GoodsId(arg.GoodsId) : null;
        Quantity = arg.Quantity;
        ServiceDuration = arg.ServiceDuration;
        UsageDuration = arg.UsageDuration;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static LogisticsRequestGoods Create(CreateLogisticsRequestGoodsArg arg)
    {
        CreateGuards(arg);
        return new LogisticsRequestGoods(arg);
    }
    #region Guards
    private static void CreateGuards(CreateLogisticsRequestGoodsArg arg)
    {
        arg.NullCheck();

        if (arg.Quantity == 0)
            throw new SimaResultException(code: CodeMessges._100070Code, message: Messages.QuantityNullError);      
    }
    #endregion
    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public void ChangeGoodsStatus(GoodsStatusEnum status)
    {
        GoodsStatusId = new GoodsStatusId((long)status);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public LogisticsRequestGoodsId Id { get; private set; }
    public GoodsCategoryId GoodsCategoryId { get; private set; }
    public virtual GoodsCategory GoodsCategory { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public GoodsStatusId? GoodsStatusId { get; private set; }
    public virtual GoodsStatus GoodsStatus { get; private set; }
    public GoodsId? GoodsId { get; private set; }
    public virtual Goods? Goods { get; private set; }
    public float Quantity { get; private set; }
    public float? ServiceDuration { get; private set; }
    public int? UsageDuration { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<LogisticsSupplyGoods> _logisticsSupplyGoods = new();
    public ICollection<LogisticsSupplyGoods> LogisticsSupplyGoods => _logisticsSupplyGoods;
}
