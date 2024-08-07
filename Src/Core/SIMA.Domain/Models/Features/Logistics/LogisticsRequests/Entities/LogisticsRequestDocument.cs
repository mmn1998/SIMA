using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class LogisticsRequestDocument : Entity
{

    private  LogisticsRequestDocument()
    {
    }
    private  LogisticsRequestDocument(CreateLogisticsRequestDocumentArg arg)
    {
        Id = new  LogisticsRequestDocumentId(IdHelper.GenerateUniqueId());
        LogisticsRequestId = new LogisticsRequestId(arg.LogisticsRequestId);
        DocumentId= new DocumentId(arg.DocumentId); 
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LogisticsRequestDocument> Create(CreateLogisticsRequestDocumentArg arg)
    {
        return new LogisticsRequestDocument(arg);
    }

    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

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
