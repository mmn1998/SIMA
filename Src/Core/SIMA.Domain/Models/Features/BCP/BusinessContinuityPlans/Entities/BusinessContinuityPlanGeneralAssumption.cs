using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanGeneralAssumption : Entity
{
    private BusinessContinuityPlanGeneralAssumption() { }
    private BusinessContinuityPlanGeneralAssumption(CreateBusinessContinuityPlanGeneralAssumptionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanGeneralAssumption Create(CreateBusinessContinuityPlanGeneralAssumptionArg arg)
    {
        return new BusinessContinuityPlanGeneralAssumption(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanGeneralAssumptionArg arg)
    {
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessContinuityGeneralAssumptionId Id { get; private set; }
    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
    
    public string? Title { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
