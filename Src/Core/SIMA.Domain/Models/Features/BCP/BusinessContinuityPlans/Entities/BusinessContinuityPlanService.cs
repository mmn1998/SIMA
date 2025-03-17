using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanService : Entity
{
    private BusinessContinuityPlanService() { }
    private BusinessContinuityPlanService(CreateBusinessContinuityPlanServiceArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanId = new(arg.BusinessContinuityPlanId);
        ServiceId = new(arg.ServiceId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanService Create(CreateBusinessContinuityPlanServiceArg arg)
    {
        return new BusinessContinuityPlanService(arg);
    }

    public void ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public BusinessContinuityPlanServiceId Id { get; private set; }
    public BusinessContinuityPlanId BusinessContinuityPlanId { get; private set; }
    public virtual BusinessContinuityPlan BusinessContinuityPlan { get; private set; }
    public ServiceId ServiceId { get; private set; }
    public virtual Service Service { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    
}