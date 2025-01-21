using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;

public class Resource : Entity
{
    private Resource()
    {

    }
    private Resource(CreateResourceArg arg)
    {
        Id = new(arg.Id);
        AccountTypeId = new(arg.AccountTypeId);
        CurrencyTypeId = new(arg.CurrencyTypeId);
        BrokerId = new(arg.BrokerId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Title = arg.Title;
        Code = arg.Code;
        AccountNumber = arg.AccountNumber;
        AvaliableBalance = arg.AvaliableBalance;
        BlockedBalance = arg.BlockedBalance;
        CurrentBalance = arg.CurrentBalance;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Resource> Create(CreateResourceArg arg, IResourceDomainService service)
    {
        await CreateGuards(arg, service);
        return new Resource(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateResourceArg arg, IResourceDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.AccountNumber.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyResourceArg arg, IResourceDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();
        arg.AccountNumber.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public async Task Modify(ModifyResourceArg arg, IResourceDomainService service)
    {
        await ModifyGuards(arg, service);
        AccountTypeId = new(arg.AccountTypeId);
        CurrencyTypeId = new(arg.CurrencyTypeId);
        BrokerId = new(arg.BrokerId);
        if (arg.ParentId.HasValue) ParentId = new(arg.ParentId.Value);
        Title = arg.Title;
        Code = arg.Code;
        AccountNumber = arg.AccountNumber;
        AvaliableBalance = arg.AvaliableBalance;
        BlockedBalance = arg.BlockedBalance;
        CurrentBalance = arg.CurrentBalance;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public ResourceId Id { get; private set; }
    public BrokerId BrokerId { get; private set; }
    public virtual Broker Broker { get; private set; }
    public AccountTypeId AccountTypeId { get; private set; }
    public virtual AccountType AccountType { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public ResourceId? ParentId { get; private set; }
    public string AccountNumber { get; private set; }
    public CurrencyTypeId CurrencyTypeId { get; private set; }
    public virtual CurrencyType CurrencyType { get; private set; }
    public decimal CurrentBalance { get; private set; }
    public decimal AvaliableBalance { get; private set; }
    public decimal BlockedBalance { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<TrustyDraftResource> _trustyDraftResources = new();
    public ICollection<TrustyDraftResource> TrustyDraftResources => _trustyDraftResources;

    private List<ResourceTransactionHistory> _transactionHistories = new();
    public ICollection<ResourceTransactionHistory> TransactionHistories => _transactionHistories;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
