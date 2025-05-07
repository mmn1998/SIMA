using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Args;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.Severities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;

public class Severity : Entity, IAggregateRoot
{
    private Severity()
    {

    }
    private Severity(CreateSeverityArg arg)
    {
        Id = new(arg.Id);
        Code = arg.Code;
        SeverityValueId = new(arg.SeverityValueId);
        AffectedHistoryId = new(arg.AffectedHistoryId);
        ConsequenceLevelId = new(arg.ConsequenceLevelId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Severity> Create(CreateSeverityArg arg, ISeverityDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new Severity(arg);
    }
    public async Task Modify(ModifySeverityArg arg, ISeverityDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        SeverityValueId = new(arg.SeverityValueId);
        AffectedHistoryId = new(arg.AffectedHistoryId);
        ConsequenceLevelId = new(arg.ConsequenceLevelId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public SeverityId Id { get; private set; }
    public string? Code { get; private set; }
    public ConsequenceLevelId ConsequenceLevelId { get; private set; }
    public virtual ConsequenceLevel ConsequenceLevel { get; private set; }
    public AffectedHistoryId AffectedHistoryId { get; private set; }
    public virtual AffectedHistory AffectedHistory { get; private set; }
    public SeverityValueId SeverityValueId { get; private set; }
    public virtual SeverityValue SeverityValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateSeverityArg arg, ISeverityDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsRelationsUnique(new (arg.AffectedHistoryId),new (arg.SeverityValueId),new (arg.ConsequenceLevelId))) throw new SimaResultException(CodeMessges._400Code, Messages.CombinationOfFieldsError);
    }
    private async Task ModifyGuard(ModifySeverityArg arg, ISeverityDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsRelationsUnique(new (arg.AffectedHistoryId),new (arg.SeverityValueId),new (arg.ConsequenceLevelId),Id)) throw new SimaResultException(CodeMessges._400Code, Messages.CombinationOfFieldsError);

    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<RiskLevelCobit> _riskLevelCobits = new();
    public ICollection<RiskLevelCobit> RiskLevelCobits => _riskLevelCobits;
}