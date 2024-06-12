using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class LogisticsRequestDocument : Entity
{
    public LogisticsRequestDocumentId Id { get; private set; }
    public LogisticsRequestId LogisticsRequestId { get; private set; }
    public virtual LogisticsRequest LogisticsRequest { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<Ordering> _orderings = new();
    public ICollection<Ordering> Orderings => _orderings;
    private List<DeliveryOrder> _deliveryOrders = new();
    public ICollection<DeliveryOrder> DeliveryOrders => _deliveryOrders;
    private List<TenderResult> _tenderResults = new();
    public ICollection<TenderResult> TenderResults => _tenderResults;
    private List<SupplierContract> _supplierContracts = new();
    public ICollection<SupplierContract> SupplierContracts => _supplierContracts;
    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;
    private List<ReceiveOrder> _receiveOrders = new();
    public ICollection<ReceiveOrder> ReceiveOrders => _receiveOrders;
    private List<RequestInquiry> _requestInquiries = new();
    public ICollection<RequestInquiry> RequestInquiries => _requestInquiries;
    private List<ReturnOrder> _returnOrders = new();
    public ICollection<ReturnOrder> ReturnOrders => _returnOrders;
}
