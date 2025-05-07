using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Args;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;

public class CurrencyOprationType : Entity
{
    private CurrencyOprationType()
    {

    }
    private CurrencyOprationType(CreateCurrencyOprationTypeArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<CurrencyOprationType> Create(CreateCurrencyOprationTypeArg arg, ICurrencyOprationTypeDomainService domainService)
    {
        await CreateGuards(arg, domainService);
        return new CurrencyOprationType(arg);
    }
    public async Task Modify(ModifyCurrencyOprationTypeArg arg, ICurrencyOprationTypeDomainService domainService)
    {
        await ModifyGuards(arg, domainService);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifyAt;
        ModifiedBy = arg.ModifyBy;
    }
    public void Delete(long userId)
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ModifiedBy = userId;
    }

    #region Guards
    private static async Task CreateGuards(CreateCurrencyOprationTypeArg arg, ICurrencyOprationTypeDomainService CurrencyOprationTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await CurrencyOprationTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCurrencyOprationTypeArg arg, ICurrencyOprationTypeDomainService CurrencyOprationTypeDomainService)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);

        if (await CurrencyOprationTypeDomainService.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    #endregion

    public CurrencyOprationTypeId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<WageRate> _wageRates = new();
    public ICollection<WageRate> WageRates => _wageRates;
}
