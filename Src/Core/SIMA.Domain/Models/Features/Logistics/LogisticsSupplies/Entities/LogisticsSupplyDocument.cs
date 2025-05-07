using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.OrderingItems.Entities;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class LogisticsSupplyDocument : Entity
{
    private LogisticsSupplyDocument()
    {
        
    }
    private LogisticsSupplyDocument(CreateLogisticsSupplyDocumentArg arg)
    {
        Id = new(arg.Id);
        LogisticsSupplyId = new(arg.LogisticsSupplyId);
        DocumentId = new(arg.DocumentId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static LogisticsSupplyDocument Create(CreateLogisticsSupplyDocumentArg arg)
    {
        return new LogisticsSupplyDocument(arg);
    }
    public LogisticsSupplyDocumentId Id { get; private set; }
    public LogisticsSupplyId LogisticsSupplyId { get; private set; }
    public virtual LogisticsSupply LogisticsSupply { get; private set; }
    public DocumentId DocumentId { get; private set; }
    public virtual Document Document { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<RequestInquiry> _requestInquiries = new();
    public ICollection<RequestInquiry> RequestInquiries => _requestInquiries;

    private List<ReceiveOrder> _receiveOrders = new();
    public ICollection<ReceiveOrder> ReceiveOrders => _receiveOrders;

    private List<SupplierContract> _supplierContracts = new();
    public ICollection<SupplierContract> SupplierContracts => _supplierContracts;

    private List<Ordering> _orderings = new();
    public ICollection<Ordering> Orderings => _orderings;

    private List<TenderResult> _tenderResults = new();
    public ICollection<TenderResult> TenderResults => _tenderResults;

    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;

    private List<DeliveryItem> _deliveryItems = new();
    public ICollection<DeliveryItem> DeliveryItems => _deliveryItems;

    private List<ReturnOrderingItem> _returnOrderingItems = new();
    public ICollection<ReturnOrderingItem> ReturnOrderingItems => _returnOrderingItems;

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
