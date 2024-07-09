namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args
{
    public class CreateLogisticsRequestGoodsArg
    {
        public long LogisticsRequestId { get;  set; }
        public long GoodsId { get;  set; }
        public float Quantity { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
       
    }
}
