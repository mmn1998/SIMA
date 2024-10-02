namespace SIMA.Domain.Models.Features.Logistics.OrderingItems.Args;

public class CreateOrderingItemArg
{
    public long Id { get; set; }
    public long OrderingId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
}