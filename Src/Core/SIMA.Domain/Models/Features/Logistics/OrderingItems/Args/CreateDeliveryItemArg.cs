namespace SIMA.Domain.Models.Features.Logistics.OrderingItems.Args;

public class CreateDeliveryItemArg
{
    public long Id { get; set; }
    public long OrderingItemId { get; set; }
    public long ReceiptDocumentId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Description { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}