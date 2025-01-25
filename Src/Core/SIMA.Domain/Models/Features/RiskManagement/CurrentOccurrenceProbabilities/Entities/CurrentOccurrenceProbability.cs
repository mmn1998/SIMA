using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;

public class CurrentOccurrenceProbability : Entity, IAggregateRoot
{
    private CurrentOccurrenceProbability()
    {

    }
    private CurrentOccurrenceProbability(CreateCurrentOccurrenceProbabilityArg arg)
    {
        Id = new(arg.Id);
        Code = arg.Code;
        InherentOccurrenceProbabilityValueId = new(arg.InherentOccurrenceProbabilityValueId);
        MatrixAValueId = new(arg.MatrixAValueId);
        ScenarioHistoryId = new(arg.ScenarioHistoryId);
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<CurrentOccurrenceProbability> Create(CreateCurrentOccurrenceProbabilityArg arg, ICurrentOccurrenceProbabilityDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new CurrentOccurrenceProbability(arg);
    }
    public async Task Modify(ModifyCurrentOccurrenceProbabilityArg arg, ICurrentOccurrenceProbabilityDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        InherentOccurrenceProbabilityValueId = new(arg.InherentOccurrenceProbabilityValueId);
        MatrixAValueId = new(arg.MatrixAValueId);
        ScenarioHistoryId = new(arg.ScenarioHistoryId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public CurrentOccurrenceProbabilityId Id { get; private set; }
    public string? Code { get; private set; }
    public MatrixAValueId MatrixAValueId { get; private set; }
    public virtual MatrixAValue MatrixAValue { get; private set; }
    public ScenarioHistoryId ScenarioHistoryId { get; private set; }
    public virtual ScenarioHistory ScenarioHistory { get; private set; }
    public InherentOccurrenceProbabilityValueId InherentOccurrenceProbabilityValueId { get; private set; }
    public virtual InherentOccurrenceProbabilityValue InherentOccurrenceProbabilityValue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateCurrentOccurrenceProbabilityArg arg, ICurrentOccurrenceProbabilityDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifyCurrentOccurrenceProbabilityArg arg, ICurrentOccurrenceProbabilityDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
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
}