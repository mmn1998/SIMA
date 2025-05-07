using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Args;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Entities;

public class ScenarioHistory : Entity, IAggregateRoot
{
    private ScenarioHistory()
    {

    }
    private ScenarioHistory(CreateScenarioHistoryArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        NumericValue = arg.NumericValue;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<ScenarioHistory> Create(CreateScenarioHistoryArg arg, IScenarioHistoryDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new ScenarioHistory(arg);
    }
    public async Task Modify(ModifyScenarioHistoryArg arg, IScenarioHistoryDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ValueTitle = arg.ValueTitle;
        NumericValue = arg.NumericValue;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public ScenarioHistoryId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public float NumericValue { get; private set; }
    public string? ValueTitle { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateScenarioHistoryArg arg, IScenarioHistoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsNumericUnique(arg.NumericValue)) throw new SimaResultException(CodeMessges._400Code, Messages.NumericValueNotUniqueError);
    }
    private async Task ModifyGuard(ModifyScenarioHistoryArg arg, IScenarioHistoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ValueTitle.NullCheck();
        if (arg.NumericValue == 0) throw SimaResultException.NullException;
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
    private List<InherentOccurrenceProbability> _inherentOccurrenceProbabilities = new();
    public ICollection<InherentOccurrenceProbability> InherentOccurrenceProbabilities => _inherentOccurrenceProbabilities;
    private List<Risk> _risks = new();
    public ICollection<Risk> Risks => _risks;
}