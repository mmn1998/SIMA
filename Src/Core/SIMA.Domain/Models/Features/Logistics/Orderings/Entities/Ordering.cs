using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.Orderings.Entities;

public class Ordering : Entity
{
    public OrderingId Id { get; private set; }
    public DateTime OrderDate { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public CandidatedSupplierId CandidatedSupplierId { get; private set; }
    public virtual CandidatedSupplier CandidatedSupplier { get; private set; }
    public LogisticsRequestDocumentId ReceiptDocumentId { get; private set; }
    public virtual LogisticsRequestDocument ReceiptDocument { get; private set; }
    public string? ReceiptNumber { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<OrderingItem> _orderingItems = new();
    public ICollection<OrderingItem> OrderingItems => _orderingItems;

    private List<SupplierBlackListHistory> _supplierBlackListHistories = new();
    public ICollection<SupplierBlackListHistory> SupplierBlackListHistories => _supplierBlackListHistories;

    private List<ReceiveOrder> _receiveOrders = new();
    public ICollection<ReceiveOrder> ReceiveOrders => _receiveOrders;

    private List<PaymentCommand> _paymentCommands = new();
    public ICollection<PaymentCommand> PaymentCommands => _paymentCommands;

    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;
}