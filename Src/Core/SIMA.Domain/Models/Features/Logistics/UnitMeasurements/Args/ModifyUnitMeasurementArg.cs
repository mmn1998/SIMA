namespace SIMA.Domain.Models.Features.Logistics.GoodsTypes.Args;

public class ModifyUnitMeasurementArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}