using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Args;
using SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Contracts;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.ScenarioRecoveryCriterias.Entities;

public class ScenarioRecoveryCriteria : Entity
{
    private ScenarioRecoveryCriteria()
    {

    }
    private ScenarioRecoveryCriteria(CreateScenarioRecoveryCriteriaArg arg)
    {
        Id = new ScenarioRecoveryCriteriaId(arg.Id);
        ScenarioId = new(arg.ScenarioId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ScenarioRecoveryCriteria> Create(CreateScenarioRecoveryCriteriaArg arg, IScenarioRecoveryCriteriaDomainService service)
    {
        await CreateGuards(arg, service);
        return new ScenarioRecoveryCriteria(arg);
    }
    public async Task Modify(ModifyScenarioRecoveryCriteriaArg arg, IScenarioRecoveryCriteriaDomainService service)
    {
        await ModifyGuards(arg, service);
        ScenarioId = new(arg.ScenarioId);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateScenarioRecoveryCriteriaArg arg, IScenarioRecoveryCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyScenarioRecoveryCriteriaArg arg, IScenarioRecoveryCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ScenarioRecoveryCriteriaId Id { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public Scenario Scenario { get; private set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
