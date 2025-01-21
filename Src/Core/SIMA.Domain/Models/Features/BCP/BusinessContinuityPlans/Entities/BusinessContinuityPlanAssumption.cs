using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanAssumption : Entity
{
    private BusinessContinuityPlanAssumption() { }
    private BusinessContinuityPlanAssumption(CreateBusinessContinuityPlanAssumptionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanAssumption Create(CreateBusinessContinuityPlanAssumptionArg arg)
    {
        return new BusinessContinuityPlanAssumption(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanAssumptionArg arg)
    {
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }
    public BusinessContinuityPlanAssumptionId Id { get; private set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
    
    public string Title { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ScenarioBusinessContinuityPlanAssumption> _scenarioBusinessContinuityPlanAssumption = new();
    public ICollection<ScenarioBusinessContinuityPlanAssumption> ScenarioBusinessContinuityPlanAssumptions => _scenarioBusinessContinuityPlanAssumption;

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
