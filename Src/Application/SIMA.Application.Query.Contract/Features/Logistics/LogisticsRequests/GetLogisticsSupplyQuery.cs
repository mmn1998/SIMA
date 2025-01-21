using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests
{

    public class GetLogisticsSupplyQuery
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public long? UnitMeasurementId { get; set; }
        public string? UnitMeasurementName { get; set; }
        public long? GoodsTypeId { get; set; }
        public string? GoodsTypeName { get; set; }
        public long? GoodsCategoryId { get; set; }
        public string? GoodsCategoryName { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public long? LogisticsRequestId { get; set; }
        public string? IsContractRequired { get; set; }
        public string? IsPrepaymentRequired { get; set; }
        public int? PrepaymentPrecentage { get; set; }
        public string? LogisticsRequestIdCode { get; set; }
        public DateTime? RequestDateTime { get; set; }
        public string? RequestPersianDateTime => DateHelper.ToPersianDateTime(RequestDateTime);
        public decimal? EstimatedPrice { get; set; }
    }

}
