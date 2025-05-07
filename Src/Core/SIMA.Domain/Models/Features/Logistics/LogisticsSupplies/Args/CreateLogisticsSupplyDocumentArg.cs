namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;

public class CreateLogisticsSupplyDocumentArg
{
    public long Id { get; set; }
    public long LogisticsSupplyId { get; set; }
    public long DocumentId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}