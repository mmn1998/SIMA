namespace SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Args;

public class ModifyGoodsStatusArg
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsRequiredItConfirmation { get; set; }
    public string? IsFinal { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
