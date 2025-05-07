using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class BusinessContinuityPlanScenarioCobitScenario : Entity
{
    private BusinessContinuityPlanScenarioCobitScenario()
    {

    }
    private BusinessContinuityPlanScenarioCobitScenario(CreateBusinessContinuityPlanScenarioCobitScenarioArg arg)
    {
        Id = new(arg.Id);
        ScenarioId = new(arg.ScenarioId);
        CobitScenarioId = new(arg.CobitScenarioId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static BusinessContinuityPlanScenarioCobitScenario Create(CreateBusinessContinuityPlanScenarioCobitScenarioArg arg)
    {
        return new BusinessContinuityPlanScenarioCobitScenario(arg);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public BusinessContinuityPlanScenarioCobitScenarioId Id { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
    public CobitScenarioId CobitScenarioId { get; private set; }
    public virtual CobitScenario CobitScenario { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<ScenarioBusinessContinuityPlanAssumption> _scenarioBusinessContinuityPlanAssumptions = new();
    public ICollection<ScenarioBusinessContinuityPlanAssumption> ScenarioBusinessContinuityPlanAssumptions => _scenarioBusinessContinuityPlanAssumptions;
}
