namespace SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;

public class GoodsList
{
    public long Id { get; set; }
    public int Quantity { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public long UnitMeasurementId { get; set; }
    public string? UnitMeasurementName { get; set; }
    public long GoodsTypeId { get; set; }
    public string? GoodsTypeName { get; set; }
    public long GoodsCategoryId { get; set; }
    public string? GoodsCategoryName { get; set; }
    public string? IsRequiredSecurityCheck { get; set; }
    public string? IsFixedAsset { get; set; }
}



