using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;

public class SeverityValue : Entity, IAggregateRoot
{
    private SeverityValue()
    {

    }
    private SeverityValue(CreateSeverityValueArg arg)
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
    public static async Task<SeverityValue> Create(CreateSeverityValueArg arg, ISeverityValueDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new SeverityValue(arg);
    }
    public async Task Modify(ModifySeverityValueArg arg, ISeverityValueDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ValuingIntervalTitle = arg.ValuingIntervalTitle;
        Color = arg.Color;
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public SeverityValueId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public string? ValuingIntervalTitle { get; private set; }
    public string? Color { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateSeverityValueArg arg, ISeverityValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValuingIntervalTitle.NullCheck();
        arg.Color.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifySeverityValueArg arg, ISeverityValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValuingIntervalTitle.NullCheck();
        arg.Color.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Color.Length > 10) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<Severity> _severities = new();
    public ICollection<Severity> Severities => _severities;
}