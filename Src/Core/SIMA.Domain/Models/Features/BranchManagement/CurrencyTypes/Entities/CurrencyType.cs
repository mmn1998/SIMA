using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;

public class CurrencyType : Entity
{
    private CurrencyType()
    {

    }

    private CurrencyType(CreateCurrencyTypeArg arg)
    {
        Id = new CurrencyTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        IsBaseCurrency = arg.IsBaseCurrency;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<CurrencyType> Create(CreateCurrencyTypeArg arg, ICurrencyTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new CurrencyType(arg);
    }
    public async Task Modify(ModifyCurrencyTypeArg arg, ICurrencyTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        IsBaseCurrency = arg.IsBaseCurrency;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public async Task Delete()
    {
        ActiveStatusId = 2;
    }
    public CurrencyTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? IsBaseCurrency { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    #region Guards
    private static async Task CreateGuards(CreateCurrencyTypeArg arg, ICurrencyTypeDomainService currencyTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (await currencyTypeDomainService.IsBaseCurrencyUnique())
        {
            if (arg.IsBaseCurrency == "1")
            {
                var baseCurrencyType = await currencyTypeDomainService.IsBaseCurrency();
                baseCurrencyType.IsBaseCurrency = "0";
            }
        }

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await currencyTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCurrencyTypeArg arg, ICurrencyTypeDomainService currencyTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (await currencyTypeDomainService.IsBaseCurrencyUnique())
        {
            if (arg.IsBaseCurrency == "1")
            {
                var baseCurrencyType = await currencyTypeDomainService.IsBaseCurrency();
                baseCurrencyType.IsBaseCurrency = "0";
            }
        }

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await currencyTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }

    #endregion


}
