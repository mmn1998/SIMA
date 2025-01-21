using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;

public class FinancialActionType : Entity
{
    private FinancialActionType()
    {

    }
    private FinancialActionType(CreateFinancialActionTypeArg arg)
    {
        Id = new FinancialActionTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<FinancialActionType> Create(CreateFinancialActionTypeArg arg, IFinancialActionTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new FinancialActionType(arg);
    }
    public async Task Modify(ModifyFinancialActionTypeArg arg, IFinancialActionTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete(long userId)
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    #region Guards
    private static async Task CreateGuards(CreateFinancialActionTypeArg arg, IFinancialActionTypeDomainService FinancialActionTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await FinancialActionTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyFinancialActionTypeArg arg, IFinancialActionTypeDomainService FinancialActionTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await FinancialActionTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    #endregion

    public FinancialActionTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ResourceTransactionHistory> _transactionHistories = new();
    public ICollection<ResourceTransactionHistory> TransactionHistories => _transactionHistories;
}
