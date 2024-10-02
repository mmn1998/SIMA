namespace SIMA.Application.Contract.Features.Logistics.LogisticRequests;

public class LogisticRequestGoodsCommand
{
    public long GoodsId { get; set; }
    public long Quantity { get; set; }
    public decimal EachPrice { get; set; }
}
