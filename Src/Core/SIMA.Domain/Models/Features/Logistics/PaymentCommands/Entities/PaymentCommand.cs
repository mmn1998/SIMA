using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;

public class PaymentCommand : Entity, IAggregateRoot
{
    public PaymentCommandId Id { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }

    public DateTime CommandDate { get; private set; }
    public string? CommandDescription { get; private set; }
    public string? Description { get; private set; }
    public string? IsPrePayment { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;
}
