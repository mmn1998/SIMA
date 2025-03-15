using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;

public class RiskLevelCobit : Entity, IAggregateRoot
{
    private RiskLevelCobit()
    {

    }
    private RiskLevelCobit(CreateRiskLevelCobitArg arg)
    {
        Id = new(arg.Id);
        Code = arg.Code;
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
        SeverityId = new(arg.SeverityId);
        RiskLevelId = new(arg.RiskLevelId);
        NumericValue = arg.NumericValue;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<RiskLevelCobit> Create(CreateRiskLevelCobitArg arg, IRiskLevelCobitDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new RiskLevelCobit(arg);
    }
    public async Task Modify(ModifyRiskLevelCobitArg arg, IRiskLevelCobitDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
        SeverityId = new(arg.SeverityId);
        RiskLevelId = new(arg.RiskLevelId);
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public RiskLevelCobitId Id { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public SeverityId SeverityId { get; private set; }
    public virtual Severity Severity { get; private set; }
    public RiskLevelId RiskLevelId { get; private set; }
    public virtual RiskLevel RiskLevel { get; private set; }
    public CurrentOccurrenceProbabilityValueId CurrentOccurrenceProbabilityValueId{ get; private set; }
    public virtual CurrentOccurrenceProbabilityValue CurrentOccurrenceProbabilityValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateRiskLevelCobitArg arg, IRiskLevelCobitDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    private async Task ModifyGuard(ModifyRiskLevelCobitArg arg, IRiskLevelCobitDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
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
}