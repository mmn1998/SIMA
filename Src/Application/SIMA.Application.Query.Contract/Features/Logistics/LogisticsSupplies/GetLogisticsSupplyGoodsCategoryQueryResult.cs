namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsSupplies
{
    public class GetLogisticsSupplyGoodsCategoryQueryResult
    {

        public int Index { get; set; }
        public long LogisticsSupplyGoodsId { get; set; }
        public long? LogisticsRequestGoodsId { get; set; }
        public string? LogisticsRequestCode { get; set; }
        public long? GoodsCategoryId { get; set; }
        public string? GoodsCategoryName { get; set; }
        public long? GoodsId { get; set; }
        public string? GoodsName { get; set; }
        public long? UnitMeasurementId { get; set; }
        public string? UnitMeasurementName { get; set; }
        public int? Quantity { get; set; }
        public double? EstimatedPrice { get; set; }
        public string? IsContractRequired { get; set; }
        public string? IsPrePaymentRequired { get; set; }
        public int? PrePaymentPercentage { get; set; }
        public string? PayByFundCard { get; set; }
    }
}
