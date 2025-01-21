using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class ScenarioBusinessContinuityPlanVersioning : Entity
{
    private ScenarioBusinessContinuityPlanVersioning()
    {

    }
    private ScenarioBusinessContinuityPlanVersioning(CreateScenarioBusinessContinuityPlanVersioningArg arg)
    {
        Id = new(arg.Id);
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ScenarioId = new(arg.ScenarioId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ScenarioBusinessContinuityPlanVersioning Create(CreateScenarioBusinessContinuityPlanVersioningArg arg)
    {
        return new ScenarioBusinessContinuityPlanVersioning(arg);
    }

    public void Modify(ModifyScenarioBusinessContinuityPlanVersioningArg arg)
    {
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ScenarioId = new(arg.ScenarioId);
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
    public ScenarioBusinessContinuityPlanVersioningId Id { get; set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public Scenario Scenario { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
