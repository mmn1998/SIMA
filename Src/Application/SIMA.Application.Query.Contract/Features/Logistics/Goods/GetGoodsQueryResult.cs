namespace SIMA.Application.Query.Contract.Features.Logistics.Goods;

public class GetGoodsQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long GoodsCategoryId { get; set; }
    public string? GoodsCategory { get; set; }
    public long GoodsTypeId { get; set; }
    public string? GoodsTypeName { get; set; }
    public long UnitMeasurementId { get; set; }
    public string? UnitMeasurement { get; set; }
    public string? ActiveStatus { get; set; }
}