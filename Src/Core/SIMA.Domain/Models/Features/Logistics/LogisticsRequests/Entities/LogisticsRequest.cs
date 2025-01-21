using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Goodses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.PriceEstimations.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
        AddDomainEvent(new CreateLogisticsRequestEvent(arg.IssueId, MainAggregateEnums.LogisticsRequest, Description, Id.Value, arg.IssuePreorityId, arg.DueDate , arg.RequesterId));
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
        AddDomainEvent(new ModifyLogisticsRequestEvent(arg.IssueId, MainAggregateEnums.LogisticsRequest, arg.IssuePreorityId, arg.DueDate, arg.Weight, arg.Description));
    }

    public async Task AddRequestGoods(List<CreateLogisticsRequestGoodsArg> args, long LogisticsRequestId, long userId, ILogisticsRequestDomainService service)
    {
        #region Guard For Goods
        //سیستم نمی تواند درخواستی را ثبت نماید که کالاهای آن به لحاظ ویژگی و تنظیمات  با یکدیگر متفاوت باشند.
        var goodsId = args.Select(it => it.GoodsCategoryId).ToList();
        bool isTechnological = await service.IsTechnological(goodsId);
        if (!isTechnological) { throw new SimaResultException(CodeMessges._400Code, Messages.GoodsConfigMustBeTheSame); }
        bool isGoods = await service.IsGoods(goodsId);
        if (!isGoods) { throw new SimaResultException(CodeMessges._400Code, Messages.GoodsConfigMustBeTheSame); }
        bool isHardware = await service.IsHardware(goodsId);
        if (!isHardware) { throw new SimaResultException(CodeMessges._400Code, Messages.GoodsConfigMustBeTheSame); }


        //طبق گفته تحلیل ولیدیشن و بررسی سمت فرانت می باشد
        //await service.IsCheckDurationGoodsCategory(args);


        #endregion
        var previousLogisticsRequestGoods = _logisticsRequestGoods.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addGoods = args.Where(x => !previousLogisticsRequestGoods.Any(c => c.GoodsCategoryId.Value == x.GoodsCategoryId)).ToList();
        var deleteGoods = previousLogisticsRequestGoods.Where(x => !args.Any(c => c.GoodsCategoryId == x.GoodsCategoryId.Value)).ToList();

        foreach (var goods in addGoods)
        {
            var entity = _logisticsRequestGoods.Where(x => (x.GoodsCategoryId == new GoodsCategoryId(goods.GoodsCategoryId) && x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = Entities.LogisticsRequestGoods.Create(goods);
                _logisticsRequestGoods.Add(entity);
            }
        }

        foreach (var goods in deleteGoods)
        {
            goods.Delete(userId);
        }
    }

    public async Task ChangeGoodsStatus(long logisticsRequestGoodsId , GoodsStatusEnum goodsStatus)
    {
        var requestGoods = _logisticsRequestGoods.Where(x => x.Id == new LogisticsRequestGoodsId(logisticsRequestGoodsId)).FirstOrDefault();
        requestGoods.ChangeGoodsStatus(goodsStatus);
    }
    public void AddLogisticsRequestDocument(List<CreateLogisticsRequestDocumentArg> args, long LogisticsRequestId, long userId)
    {
        var previousLogisticsRequestDocumentGoods = _logisticsRequestDocuments.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addDocument = args.Where(x => !previousLogisticsRequestDocumentGoods.Any(c => c.DocumentId.Value == x.DocumentId)).ToList();
        var deleteDocument = previousLogisticsRequestDocumentGoods.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value)).ToList();


        foreach (var document in addDocument)
        {
            var entity = _logisticsRequestDocuments.Where(x => (x.DocumentId == new DocumentId(document.DocumentId) && x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = LogisticsRequestDocument.Create(document);
                _logisticsRequestDocuments.Add(entity);
            }
        }

        foreach (var document in deleteDocument)
        {
            document.Delete(userId);
        }
    }
    public void DeleteRequestGoods(long LogisticsRequestId, long loginUserId)
    {
        var logisticsRequestGoods = _logisticsRequestGoods.Where(x => x.LogisticsRequestId == new LogisticsRequestId(LogisticsRequestId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        foreach (var goods in logisticsRequestGoods)
        {
            goods.Delete(loginUserId);
        }
    }
    public void DeleteLogisticsRequestDocument(long LogisticsRequestId, long loginUserId)
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
    private List<PriceEstimation> _priceEstimations = new();
    public ICollection<PriceEstimation> PriceEstimations => _priceEstimations;
    private List<LogisticsRequestDocument> _logisticsRequestDocuments = new();
    public ICollection<LogisticsRequestDocument> LogisticsRequestDocuments => _logisticsRequestDocuments;
    
    //private List<DeliveryOrder> _deliveryOrders = new();
    //public ICollection<DeliveryOrder> DeliveryOrders => _deliveryOrders;
    //private List<ReturnOrder> _returnOrders = new();
    //public ICollection<ReturnOrder> ReturnOrders => _returnOrders;

    private List<LogisticsRequestGoods> _logisticsRequestGoods = new();
    public ICollection<LogisticsRequestGoods> LogisticsRequestGoods => _logisticsRequestGoods;
}
