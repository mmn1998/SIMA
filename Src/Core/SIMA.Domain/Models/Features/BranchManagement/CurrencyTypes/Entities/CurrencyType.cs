using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Interfaces;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

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
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
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

    private List<WageRate> _wageRates = new();
    public ICollection<WageRate> WageRates => _wageRates;

    private List<Resource> _resources = new();
    public ICollection<Resource> Resources => _resources;

    private List<TrustyDraft> _trustyDrafts = new();
    public ICollection<TrustyDraft> TrustyDrafts => _trustyDrafts;

    private List<TrustyDraft> _netTrustyDrafts = new();
    public ICollection<TrustyDraft> NetTrustyDrafts => _netTrustyDrafts;

    private List<TrustyDraft> _cancellationTrustyDrafts = new();
    public ICollection<TrustyDraft> CancellationTrustyDrafts => _cancellationTrustyDrafts;

    private List<InquiryRequestCurrency> _inquiryRequestCurrencies = new();
    public ICollection<InquiryRequestCurrency> InquiryRequestCurrencies => _inquiryRequestCurrencies;

    private List<InquiryRequest> _inquiryRequests = new();
    public ICollection<InquiryRequest> InquiryRequests => _inquiryRequests;

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
