using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.ValueObjects;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;

public class BusinessContinuityPlanCriticalActivity : Entity
{
    private BusinessContinuityPlanCriticalActivity() { }
    private BusinessContinuityPlanCriticalActivity(CreateBusinessContinuityPlanCriticalActivityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        BusinessContinuityPlanVersioningId = new(arg.BusinessContinuityPlanVersioningId);
        CriticalActivityId = new(arg.CriticalActivityId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static BusinessContinuityPlanCriticalActivity Create(CreateBusinessContinuityPlanCriticalActivityArg arg)
    {
        return new BusinessContinuityPlanCriticalActivity(arg);
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
    public BusinessContinuityPlanCriticalActivityId Id { get; private set; }
    public BusinessContinuityPlanVersioningId BusinessContinuityPlanVersioningId { get; private set; }
    public virtual BusinessContinuityPlanVersioning BusinessContinuityPlanVersioning { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
   
}
