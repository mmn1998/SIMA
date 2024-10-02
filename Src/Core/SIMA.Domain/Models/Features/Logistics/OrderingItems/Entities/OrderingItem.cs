using SIMA.Domain.Models.Features.Logistics.OrderingItems.Args;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;

public class OrderingItem : Entity
{
    private OrderingItem()
    {
        
    }
    private OrderingItem(CreateOrderingItemArg arg)
    {
        Id = new(arg.Id);
        OrderingId = new(arg.OrderingId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static OrderingItem Create(CreateOrderingItemArg arg)
    {
        return new OrderingItem(arg);
    }
    public OrderingItemId Id { get; private set; }
    public virtual Ordering Ordering { get; private set; }
    public OrderingId OrderingId { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    private List<DeliveryItem> _deliveryItems = new();
    public ICollection<DeliveryItem> DeliveryItems => _deliveryItems;
    private List<ReturnOrderingItem> _returnOrderingItems = new();
    public ICollection<ReturnOrderingItem> ReturnOrderingItems => _returnOrderingItems;
}