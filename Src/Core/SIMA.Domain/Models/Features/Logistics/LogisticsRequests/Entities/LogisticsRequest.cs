using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class LogisticsRequest : Entity, IAggregateRoot
{
    private LogisticsRequest() { }
    private LogisticsRequest(CreateLogisticsRequestArg arg)
    {
        Id = new(arg.Id);
        Description = arg.Description;
        Code = arg.Code;
        IssueId = new(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<LogisticsRequest> Create(CreateLogisticsRequestArg arg, ILogisticsRequestDomainService service)
    {
        await CreateGuards(arg, service);
        return new LogisticsRequest(arg);
    }
    public async Task Modify(ModifyLogisticsRequestArg arg, ILogisticsRequestDomainService service)
    {
        await ModifyGuards(arg, service);
        Description = arg.Description;
        Code = arg.Code;
        IssueId = new(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateLogisticsRequestArg arg, ILogisticsRequestDomainService service)
    {

    }
    private async Task ModifyGuards(ModifyLogisticsRequestArg arg, ILogisticsRequestDomainService service)
    {

    }
    #endregion
    public LogisticsRequestId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public string? Description { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Goods> _goods = new();
    public ICollection<Goods> Goods => _goods;
    private List<PaymentCommand> _paymentCommands = new();
    public ICollection<PaymentCommand> PaymentCommands => _paymentCommands;
    private List<PriceEstimation> _priceEstimations = new();
    public ICollection<PriceEstimation> PriceEstimations => _priceEstimations;
    private List<LogisticsRequestDocument> _logisticsRequestDocuments = new();
    public ICollection<LogisticsRequestDocument> LogisticsRequestDocuments => _logisticsRequestDocuments;
    private List<SupplierBlackListHistory> _supplierBlackListHistories = new();
    public ICollection<SupplierBlackListHistory> SupplierBlackListHistories => _supplierBlackListHistories;
    private List<CandidatedSupplier> _candidatedSuppliers = new();
    public ICollection<CandidatedSupplier> CandidatedSuppliers => _candidatedSuppliers;
    private List<GoodsCoding> _goodsCodings = new();
    public ICollection<GoodsCoding> GoodsCodings => _goodsCodings;
    private List<Ordering> _orderings = new();
    public ICollection<Ordering> Orderings => _orderings;
    private List<DeliveryOrder> _deliveryOrders = new();
    public ICollection<DeliveryOrder> DeliveryOrders => _deliveryOrders;
    private List<ReturnOrder> _returnOrders = new();
    public ICollection<ReturnOrder> ReturnOrders => _returnOrders;
    private List<ReceiveOrder> _receiveOrders = new();
    public ICollection<ReceiveOrder> ReceiveOrders => _receiveOrders;
    private List<TenderResult> _tenderResults = new();
    public ICollection<TenderResult> TenderResults => _tenderResults;
    private List<SupplierContract> _supplierContracts = new();
    public ICollection<SupplierContract> SupplierContracts => _supplierContracts;
    private List<PaymentHistory> _paymentHistories = new();
    public ICollection<PaymentHistory> PaymentHistories => _paymentHistories;
    private List<RequestInquiry> _requestInquiries = new();
    public ICollection<RequestInquiry> RequestInquiries => _requestInquiries;
    private List<LogisticsRequestGoods> _logisticsRequestGoods = new();
    public ICollection<LogisticsRequestGoods> LogisticsRequestGoods => _logisticsRequestGoods;
}
