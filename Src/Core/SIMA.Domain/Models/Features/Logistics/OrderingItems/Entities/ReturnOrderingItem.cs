using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Args;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;

public class ReturnOrderingItem : Entity
{
    private ReturnOrderingItem()
    {
        
    }
    private ReturnOrderingItem(CreateReturnOrderingItemArg arg)
    {
        Id = new(arg.Id);
        OrderingItemId = new(arg.OrderingItemId);
        ReciptDocumentId = new(arg.ReceiptDocumentId);
        ReturnQuantity = ReturnQuantity;
        Description = arg.Description;
        ReturnDate = arg.ReturnDate;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static ReturnOrderingItem Create(CreateReturnOrderingItemArg arg)
    {
        return new ReturnOrderingItem(arg);
    }
    public ReturnOrderingItemId Id { get; private set; }
    public OrderingItemId OrderingItemId { get; private set; }
    public virtual OrderingItem OrderingItem { get; private set; }
    public int ReturnQuantity { get; private set; }
    public LogisticsSupplyDocumentId? ReciptDocumentId { get; private set; }
    public virtual LogisticsSupplyDocument? ReciptDocument { get; private set; }
    public DateTime ReturnDate { get; private set; }
    public string? Description { get; private set; }
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
}
