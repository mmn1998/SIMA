using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Args;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.TransactionHistories.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;

public class FinancialSupplier : Entity
{
    private FinancialSupplier()
    {

    }
    private FinancialSupplier(CreateFinancialSupplierArg arg)
    {
        Id = new FinancialSupplierId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        CustomerId = new(arg.CustomerId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<FinancialSupplier> Create(CreateFinancialSupplierArg arg, IFinancialSupplierDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new FinancialSupplier(arg);
    }
    public async Task Modify(ModifyFinancialSupplierArg arg, IFinancialSupplierDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        CustomerId = new(arg.CustomerId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete(long userId)
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    #region Guards
    private static async Task CreateGuards(CreateFinancialSupplierArg arg, IFinancialSupplierDomainService financialSupplierDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await financialSupplierDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyFinancialSupplierArg arg, IFinancialSupplierDomainService financialSupplierDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await financialSupplierDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    #endregion

    public FinancialSupplierId Id { get; private set; }
    public CustomerId? CustomerId { get; private set; }
    public virtual Customer? Customer { get; private set; }
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
