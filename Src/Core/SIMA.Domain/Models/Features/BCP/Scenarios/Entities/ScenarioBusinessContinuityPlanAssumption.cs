using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class ScenarioBusinessContinuityPlanAssumption : Entity
{
    private ScenarioBusinessContinuityPlanAssumption()
    {

    }
    private ScenarioBusinessContinuityPlanAssumption(CreateScenarioBusinessContinuityPlanAssumptionArg arg)
    {
        Id = new(arg.Id);
        ScenarioId = new(arg.ScenarioId);
        BusinessContinuityPlanAssumptionId = new(arg.BusinessContinuityPlanAssumptionId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ScenarioBusinessContinuityPlanAssumption Create(CreateScenarioBusinessContinuityPlanAssumptionArg arg)
    {
        return new ScenarioBusinessContinuityPlanAssumption(arg);
    }
    public void Modify(ModifyScenarioBusinessContinuityPlanAssumptionArg arg)
    {
        ScenarioId = new(arg.ScenarioId);
        BusinessContinuityPlanAssumptionId = new(arg.BusinessContinuityPlanAssumptionId);
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

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public ScenarioBusinessContinuityPlanAssumptionId Id { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
    public BusinessContinuityPlanAssumptionId BusinessContinuityPlanAssumptionId { get; private set; }
    public virtual BusinessContinuityPlanAssumption BusinessContinuityPlanAssumption { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
