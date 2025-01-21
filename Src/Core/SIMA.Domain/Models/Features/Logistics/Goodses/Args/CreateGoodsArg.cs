namespace SIMA.Domain.Models.Features.Logistics.Goodses.Args;

public class CreateGoodsArg
{
    public long Id { get; set; }
    public long GoodsCategoryId { get; set; }
    public long UnitMeasurementId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}