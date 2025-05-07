using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;

public class CurrentOccurrenceProbabilityValue : Entity, IAggregateRoot
{
    private CurrentOccurrenceProbabilityValue()
    {

    }
    private CurrentOccurrenceProbabilityValue(CreateCurrentOccurrenceProbabilityValueArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        Color = arg.Color;
        ValuingIntervalTitle = arg.ValuingIntervalTitle;
        NumericValue = arg.NumericValue;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<CurrentOccurrenceProbabilityValue> Create(CreateCurrentOccurrenceProbabilityValueArg arg, ICurrentOccurrenceProbabilityValueDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new CurrentOccurrenceProbabilityValue(arg);
    }
    public async Task Modify(ModifyCurrentOccurrenceProbabilityValueArg arg, ICurrentOccurrenceProbabilityValueDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Color = arg.Color;
        ValuingIntervalTitle = arg.ValuingIntervalTitle;
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public CurrentOccurrenceProbabilityValueId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public string? Color { get; private set; }
    public string? ValuingIntervalTitle { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<RiskLevel> _riskLevels = new();
    public ICollection<RiskLevel> RiskLevels => _riskLevels;

    #region Guards
    private static async Task CreateGuadrs(CreateCurrentOccurrenceProbabilityValueArg arg, ICurrentOccurrenceProbabilityValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.Color.NullCheck();
        arg.ValuingIntervalTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    private async Task ModifyGuard(ModifyCurrentOccurrenceProbabilityValueArg arg, ICurrentOccurrenceProbabilityValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.Color.NullCheck();
        arg.ValuingIntervalTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<CurrentOccurrenceProbability> _currentOccurrenceProbabilities = new();
    public ICollection<CurrentOccurrenceProbability> CurrentOccurrenceProbabilities => _currentOccurrenceProbabilities;
    private List<RiskLevelCobit> _riskLevelCobits = new();
    public ICollection<RiskLevelCobit> RiskLevelCobits => _riskLevelCobits;
}
