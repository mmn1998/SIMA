using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequestGoodss.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PaymentCommands.Entities;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.Entities;
using SIMA.Domain.Models.Features.Logistics.Suppliers.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;
using System.Xml.Linq;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;

public class LogisticsRequest : Entity, IAggregateRoot
{
    private LogisticsRequest() { }
    private LogisticsRequest(CreateLogisticsRequestArg arg)
    {
        Id = new LogisticsRequestId(arg.Id);
        Description = arg.Description;
        Code = arg.Code;
        IssueId = new(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        AddDomainEvent(new CreateLogisticsRequestEvent(arg.IssueId, MainAggregateEnums.LogisticsRequest, Description, Id.Value , arg.IssuePreorityId , arg.Weight, arg.DueDate));
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
        AddDomainEvent(new ModifyLogisticsRequestEvent(arg.IssueId, MainAggregateEnums.LogisticsRequest, arg.IssuePreorityId, arg.DueDate,arg.Weight , arg.Description ));
    }

    public async Task AddRequestGoods(List<CreateLogisticsRequestGoodsArg> args, long LogisticsRequestId)
    {

        var previousLogisticsRequestGoods = _logisticsRequestGoods.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addGoods = args.Where(x => !previousLogisticsRequestGoods.Any(c => c.GoodsId.Value == x.GoodsId)).ToList();
        var deleteGoods = previousLogisticsRequestGoods.Where(x => !args.Any(c => c.GoodsId == x.GoodsId.Value)).ToList();


        foreach (var goods in addGoods)
        {
            var entity = _logisticsRequestGoods.Where(x => (x.GoodsId == new GoodsId(goods.GoodsId) && x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await LogisticsRequestGoodss.Entities.LogisticsRequestGoods.Create(goods);
                _logisticsRequestGoods.Add(entity);
            }
        }

        foreach (var goods in deleteGoods)
        {
            goods.Delete(args.First().CreatedBy.Value);
        }
    }
    public async Task AddLogisticsRequestDocument(List<CreateLogisticsRequestDocumentArg> args, long LogisticsRequestId)
    {
        var previousLogisticsRequestDocumentGoods = _logisticsRequestDocuments.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addDocument = args.Where(x => !previousLogisticsRequestDocumentGoods.Any(c => c.DocumentId.Value == x.DocumentId)).ToList();
        var deleteDocument = previousLogisticsRequestDocumentGoods.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value)).ToList();


        foreach (var document in addDocument)
        {
            var entity = _logisticsRequestDocuments.Where(x => (x.DocumentId == new DocumentId(document.DocumentId) && x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = await LogisticsRequestDocument.Create(document);
                _logisticsRequestDocuments.Add(entity);
            }
        }

        foreach (var document in deleteDocument)
        {
            document.Delete(args.First().CreatedBy.Value);
        }
    }

    public async Task DeleteRequestGoods(long LogisticsRequestId, long loginUserId)
    {
        var logisticsRequestGoods = _logisticsRequestGoods.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        foreach (var goods in logisticsRequestGoods)
        {
            goods.Delete(loginUserId);
        }
    }

    public async Task DeleteLogisticsRequestDocument(long LogisticsRequestId, long loginUserId)
    {
        var logisticsRequestDocument = _logisticsRequestDocuments.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        foreach (var goods in logisticsRequestDocument)
        {
            goods.Delete(loginUserId);
        }
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
    public void Delete(long issueId, long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        AddDomainEvent(new DeleteLogisticsRequestEvent(issueId));
    }
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
