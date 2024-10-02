using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class ReceiveOrder : Entity
{    
    public ReceiveOrderId Id { get; private set; }
    public DateTime ReceiveDate { get; private set; }
    public OrderingId OrderingId { get; private set; }
    public virtual Ordering Ordering { get; private set; }
    public LogisticsRequestDocumentId ReceiptDocumentId { get; private set; }
    public virtual LogisticsRequestDocument ReceiptDocument { get; private set; }
    public string? ReceiptNumber { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}