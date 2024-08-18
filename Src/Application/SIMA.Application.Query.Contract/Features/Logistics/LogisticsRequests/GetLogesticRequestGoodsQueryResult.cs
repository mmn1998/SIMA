namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests
{
    public class GetLogesticRequestGoodsQueryResult
    {
        public long Id { get; set; }
        public long Index { get; set; }
        public long LogisticsRequestId { get; set; }
        public long GoodsId { get; set; }
        public string GoodsName { get; set; }
        public string GoodsCategoryName { get; set; }
        public string GoodsCode { get; set; }
        public float Quantity { get; set; }

    }


}
