namespace SIMA.Application.Contract.Features.Logistics.LogisticRequests;

public class LogisticRequestGoodsCommand
{
    public long GoodsCategoryId { get; set; }
    public long Quantity { get; set; }
    public float ServiceDuration { get; set; }
    public int UsageDuration { get; set; }
}
