using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;

public class CriticalActivityExecutionPlan : Entity
{
    private CriticalActivityExecutionPlan()
    {
    }
    private CriticalActivityExecutionPlan(CreateCriticalActivityExecutionPlanArg arg)
    {
        Id = new CriticalActivityExecutionPlanId(arg.Id);
        CriticalActivityId = new CriticalActivityId(arg.CriticalActivityId);
        WeekyDay = arg.WeekyDay;
        ServiceAvalibilityStartTime = arg.ServiceAvalibilityStartTime;
        ServiceAvalibilityEndTime = arg.ServiceAvalibilityEndTime;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<CriticalActivityExecutionPlan> Create(CreateCriticalActivityExecutionPlanArg arg)
    {
        return new CriticalActivityExecutionPlan(arg);
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public CriticalActivityExecutionPlanId Id { get; private set; }
    public virtual CriticalActivity CriticalActivity { get; private set; }
    public CriticalActivityId CriticalActivityId { get; private set; }
    public int WeekyDay { get; private set; }
    public TimeOnly ServiceAvalibilityStartTime { get; private set; }
    public TimeOnly ServiceAvalibilityEndTime { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

}
