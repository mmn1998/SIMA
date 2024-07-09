using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanRecoveryOption : Entity
{
    private BusinessContinuityPlanRecoveryOption() { }
    private BusinessContinuityPlanRecoveryOption(CreateBusinessContinuityPlanRecoveryOptionArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanRecoveryOption Create(CreateBusinessContinuityPlanRecoveryOptionArg arg)
    {
        return new BusinessContinuityPlanRecoveryOption(arg);
    }
    public void Modify(ModifyBusinessContinuityPlanRecoveryOptionArg arg)
    {
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        ActiveStatusId = arg.ActiveStatusId;
        Title = arg.Title;
        Code = arg.Code;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public BusinessContinuityPlanRecoveryOptionId Id { get; private set; }
    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
    
    public string? Title { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}