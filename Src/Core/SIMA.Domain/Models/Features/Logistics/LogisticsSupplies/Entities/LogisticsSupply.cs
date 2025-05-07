using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Args;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Contracts;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.Orderings.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;

public class LogisticsSupply : Entity, IAggregateRoot
{
    private LogisticsSupply() { }
    private LogisticsSupply(CreateLogisticsSupplyArg arg)
    {
        Id = new(arg.Id);
        IssueId = new(arg.IssueId);
        Description = arg.Description;
        Code = arg.Code;
        PayByFundCard = arg.PayByFundCard;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        AddDomainEvent(new CreateLogisticsSupplyEvent(arg.IssueId, MainAggregateEnums.LogisticsSupply, Description, Id.Value, arg.IssuePreorityId, arg.DueDate, arg.RequesterId));
    }
    public static async Task<LogisticsSupply> Create(CreateLogisticsSupplyArg arg, ILogisticsSupplyDomainService service)
    {
        await CreateGuards(arg, service);
        return new LogisticsSupply(arg);
    }
    public async Task Modify(ModifyLogisticsSupplyArg arg, ILogisticsSupplyDomainService service)
    {
        await ModifyGuards(arg, service);
        IssueId = new(arg.IssueId);
        Description = arg.Description;
        PayByFundCard = arg.PayByFundCard;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        AddDomainEvent(new ModifyLogisticsSupplyEvent(arg.IssueId, MainAggregateEnums.LogisticsSupply, arg.IssuePreorityId, arg.DueDate, arg.Weight, arg.Description, arg.RequesterId));
    }
    #region Guards
    private static async Task CreateGuards(CreateLogisticsSupplyArg arg, ILogisticsSupplyDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyLogisticsSupplyArg arg, ILogisticsSupplyDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public LogisticsSupplyId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string PayByFundCard { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region AddMethods
    public void AddLogisticsSupplyDocuments(List<CreateLogisticsSupplyDocumentArg> args)
    {
        foreach (var arg in args)
        {
            var entity = LogisticsSupplyDocument.Create(arg);
            _logisticsSupplyDocuments.Add(entity);
        }
    }

    public void AddLogisticsSupplyGoods(List<CreateLogisticsSupplyGoodsArg> args)
    {
        foreach (var arg in args)
        {
            var entity = LogisticsSupplyGoods.Create(arg);
            _logisticsSupplyGoods.Add(entity);
        }

        AddDomainEvent(new ChangeGoodsStatusEvent(args.Select(x => x.LogisticsRequestGoodsId).ToList(), GoodsStatusEnum.CreateLogisticsSupply));
    }
    #endregion
    #region ModifyMethods
    public void ModifyLogisticsSupplyGoods(List<CreateLogisticsSupplyGoodsArg> args)
    {
        var activeEntities = _logisticsSupplyGoods.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.LogisticsRequestGoodsId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.LogisticsRequestGoodsId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _logisticsSupplyGoods.FirstOrDefault(x => x.LogisticsRequestGoodsId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = LogisticsSupplyGoods.Create(arg);
                _logisticsSupplyGoods.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    public void ModifyLogisticsSupplyDocuments(List<CreateLogisticsSupplyDocumentArg> args)
    {
        var activeEntities = _logisticsSupplyDocuments.Where(x => x.ActiveStatusId == (long)ActiveStatusEnum.Delete);
        var shouldDeleteEntities = activeEntities.Where(x => !args.Any(c => c.DocumentId == x.DocumentId.Value));
        var ShouldAddedArgs = args.Where(x => !activeEntities.Any(c => c.DocumentId.Value == x.DocumentId));
        foreach (var arg in ShouldAddedArgs)
        {
            var entity = _logisticsSupplyDocuments.FirstOrDefault(x => x.DocumentId.Value == arg.DocumentId && x.ActiveStatusId != (long)ActiveStatusEnum.Active);
            if (entity is not null)
            {
                entity.Active(arg.CreatedBy.Value);
            }
            else
            {
                entity = LogisticsSupplyDocument.Create(arg);
                _logisticsSupplyDocuments.Add(entity);
            }
        }
        foreach (var entity in shouldDeleteEntities)
        {
            entity.Delete(args[0].CreatedBy.Value);
        }
    }
    #endregion
    #region DeleteMethods
    public void DeleteLogisticsSupplyDocuments(long userId)
    {
        foreach (var entity in _logisticsSupplyDocuments)
        {
            entity.Delete(userId);
        }
    }
    public void DeleteLogisticsSupplyGoods(long userId)
    {
        foreach (var entity in _logisticsSupplyGoods)
        {
            entity.Delete(userId);
        }
    }
    #endregion   

    private List<TenderResult> _tenderResults = new();
    public ICollection<TenderResult> TenderResults => _tenderResults;

    private List<LogisticsSupplyGoods> _logisticsSupplyGoods = new();
    public ICollection<LogisticsSupplyGoods> LogisticsSupplyGoodses => _logisticsSupplyGoods;

    private List<Ordering> _ordering = new();
    public ICollection<Ordering> Orderings => _ordering;

    private List<CandidatedSupplier> _candidatedSuppliers = new();
    public ICollection<CandidatedSupplier> CandidatedSuppliers => _candidatedSuppliers;

    private List<LogisticsSupplyDocument> _logisticsSupplyDocuments = new();
    public ICollection<LogisticsSupplyDocument> LogisticsSupplyDocuments => _logisticsSupplyDocuments;
    public void Delete(long userId, long issueId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        AddDomainEvent(new DeleteLogisticsSupplytEvent(issueId));
        DeleteLogisticsSupplyDocuments(userId);
        DeleteLogisticsSupplyGoods(userId);
    }
}
