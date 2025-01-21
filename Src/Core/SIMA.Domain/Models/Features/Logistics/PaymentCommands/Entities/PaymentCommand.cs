using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;

public class PaymentCommand : Entity, IAggregateRoot
{
    public PaymentCommandId Id { get; private set; }
    public OrderingId OrderingId { get; private set; }
    public virtual Ordering Ordering { get; private set; }
    public DateTime CommandDate { get; private set; }
    public string? CommandDescription { get; private set; }
    public string? IsPrePayment { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;
}
