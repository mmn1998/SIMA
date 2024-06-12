using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class DeliveryOrder : Entity
{    
    public DeliveryOrderId Id { get; private set; }
    public DateTime DeliveryDate { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public LogisticsRequestDocumentId ReceiptDocumentId { get; private set; }
    public virtual LogisticsRequestDocument ReceiptDocument { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}