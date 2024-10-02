namespace SIMA.Domain.Models.Features.Logistics.Goodses.Args;

public class ModifyGoodsArg
{
    public long GoodsCategoryId { get; set; }
    public long UnitMeasurementId { get; set; }
    public string? IsFixedAsset { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? DurationOfConsumption { get; set; }
    public int? DurationOfService { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
