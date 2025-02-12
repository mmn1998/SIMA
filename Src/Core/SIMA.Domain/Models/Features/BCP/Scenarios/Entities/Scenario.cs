using SIMA.Domain.Models.Features.Auths.Permissions.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.BCP.Scenarios.Contracts;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class Scenario : Entity
{
    private Scenario()
    {

    }
    private Scenario(CreateScenarioArg arg)
    {
        Id = new ScenarioId(arg.Id);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Scenario> Create(CreateScenarioArg arg, IScenarioDomainService service)
    {
        await CreateGuards(arg, service);
        return new Scenario(arg);
    }
    public async Task Modify(ModifyScenarioArg arg, IScenarioDomainService service)
    {
        await ModifyGuards(arg, service);
        Title = arg.Title;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }


    #region OtherMethod

    public async Task AddScenarioBusinessContinuityPlanVersioning(List<CreateScenarioBusinessContinuityPlanVersioningArg> request, long scenarioId)
    {
        scenarioId.NullCheck();

        var previousEntity = _scenarioBusinessContinuityPlanVersioning.Where(x => x.ScenarioId == new ScenarioId(scenarioId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.BusinessContinuityPlanVersioningId.Value == x.BusinessContinuityPlanVersioningId)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.BusinessContinuityPlanVersioningId == x.BusinessContinuityPlanVersioningId.Value)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _scenarioBusinessContinuityPlanVersioning.Where(x => (x.BusinessContinuityPlanVersioningId == new BusinessContinuityPlanVersioningId(item.BusinessContinuityPlanVersioningId) && x.ScenarioId == new ScenarioId(scenarioId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = ScenarioBusinessContinuityPlanVersioning.Create(item);
                _scenarioBusinessContinuityPlanVersioning.Add(entity);
            }
        }

        foreach (var item in deleteEntity)
        {
            item.Delete((long)request[0].CreatedBy);
        }
    }

    public async Task AddScenarioBusinessContinuityPlanAssumption(List<CreateScenarioBusinessContinuityPlanAssumptionArg> request, long scenarioId)
    {
        scenarioId.NullCheck();

        var previousEntity = _scenarioBusinessContinuityPlanAssumption.Where(x => x.ScenarioId == new ScenarioId(scenarioId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addEntity = request.Where(x => !previousEntity.Any(c => c.BusinessContinuityPlanAssumptionId.Value == x.BusinessContinuityPlanAssumptionId)).ToList();
        var deleteEntity = previousEntity.Where(x => !request.Any(c => c.BusinessContinuityPlanAssumptionId == x.BusinessContinuityPlanAssumptionId.Value)).ToList();


        foreach (var item in addEntity)
        {
            var entity = _scenarioBusinessContinuityPlanAssumption.Where(x => (x.BusinessContinuityPlanAssumptionId == new BusinessContinuityPlanAssumptionId(item.BusinessContinuityPlanAssumptionId) && x.ScenarioId == new ScenarioId(scenarioId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = ScenarioBusinessContinuityPlanAssumption.Create(item);
                _scenarioBusinessContinuityPlanAssumption.Add(entity);
            }
        }

        foreach (var item in deleteEntity)
        {
            item.Delete((long)request[0].CreatedBy);
        }
    }

    public async Task AddScenarioPlanRecoveryCriteria(List<CreateScenarioRecoveryCriteriaArg> request)
    {
        if (!request.Any())
            return;
        foreach (var item in request)
        {
            if (!string.IsNullOrEmpty(item.Description))
            {
                item.ScenarioId = Id.Value;
                var issuelink = await ScenarioRecoveryCriteria.Create(item);
                _scenarioRecoveryCriteria.Add(issuelink);
            }

        }
    }

    public async Task AddScenarioPossibleAction(List<CreateScenarioPossibleActionArg> request)
    {
        if (!request.Any())
            return;
        foreach (var item in request)
        {
            if (!string.IsNullOrEmpty(item.Description))
            {
                item.ScenarioId = Id.Value;
                var entity = ScenarioPossibleAction.Create(item);
                _scenarioPossibleActions.Add(entity);
            }

        }
    }

    public async Task AddScenarioRecoveryOption(List<CreateScenarioRecoveryOptionArg> request)
    {
        if (!request.Any())
            return;
        foreach (var item in request)
        {
            if (!string.IsNullOrEmpty(item.Description))
            {
                item.ScenarioId = Id.Value;
                var issuelink = ScenarioRecoveryOption.Create(item);
                _scenarioRecoveryOption.Add(issuelink);
            }

        }
    }

    #endregion

    #region Guards
    private static async Task CreateGuards(CreateScenarioArg arg, IScenarioDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyScenarioArg arg, IScenarioDomainService service)
    {
        arg.NullCheck();
        arg.Title.NullCheck();
        arg.Code.NullCheck();

        if (arg.Title.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ScenarioId Id { get; set; }
    public string Title { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ScenarioRecoveryOption> _scenarioRecoveryOption = new();
    public ICollection<ScenarioRecoveryOption> ScenarioRecoveryOptions => _scenarioRecoveryOption;

    private List<ScenarioRecoveryCriteria> _scenarioRecoveryCriteria = new();
    public ICollection<ScenarioRecoveryCriteria> ScenarioRecoveryCriterias => _scenarioRecoveryCriteria;

    private List<ScenarioBusinessContinuityPlanAssumption> _scenarioBusinessContinuityPlanAssumption = new();
    public ICollection<ScenarioBusinessContinuityPlanAssumption> ScenarioBusinessContinuityPlanAssumptions => _scenarioBusinessContinuityPlanAssumption;

    private List<ScenarioBusinessContinuityPlanVersioning> _scenarioBusinessContinuityPlanVersioning = new();
    public ICollection<ScenarioBusinessContinuityPlanVersioning> ScenarioBusinessContinuityPlanVersionings => _scenarioBusinessContinuityPlanVersioning;

    private List<ScenarioExecutionHistory> _scenarioExecutionHistories = new();
    public ICollection<ScenarioExecutionHistory> ScenarioExecutionHistories => _scenarioExecutionHistories;

    private List<ScenarioPossibleAction> _scenarioPossibleActions = new();
    public ICollection<ScenarioPossibleAction> ScenarioPossibleActions => _scenarioPossibleActions;
    private List<CobitScenario> _cobitScenarios = new();
    public ICollection<CobitScenario> CobitScenarios => _cobitScenarios;
    private List<BusinessContinuityPlanScenarioCobitScenario> _businessContinuityPlanScenarioCobitScenarios = new();
    public ICollection<BusinessContinuityPlanScenarioCobitScenario> BusinessContinuityPlanScenarioCobitScenarios => _businessContinuityPlanScenarioCobitScenarios;
}
