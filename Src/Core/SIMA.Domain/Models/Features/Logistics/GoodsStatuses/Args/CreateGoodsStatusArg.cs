namespace SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Args;

public class CreateGoodsStatusArg
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsRequiredItConfirmation { get; set; }
    public string? IsFinal { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}
