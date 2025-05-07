using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.ValueObjects;
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
        FrequencyId = new(arg.FrequencyId);
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
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
        FrequencyId = new(arg.FrequencyId);
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public CurrentOccurrenceProbabilityId Id { get; private set; }
    public string? Code { get; private set; }
    public CurrentOccurrenceProbabilityValueId CurrentOccurrenceProbabilityValueId { get; private set; }
    public virtual CurrentOccurrenceProbabilityValue CurrentOccurrenceProbabilityValue { get; private set; }
    public FrequencyId FrequencyId { get; private set; }
    public virtual Frequency Frequency { get; private set; }
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