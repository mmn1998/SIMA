using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityExecutionPlanArg : Entity
{
    public long Id { get;  set; }
    public long CriticalActivityId { get;  set; }
    public int WeekyDay { get;  set; }
    public TimeOnly ServiceAvalibilityStartTime { get;  set; }
    public TimeOnly ServiceAvalibilityEndTime { get;  set; }
    public long ActiveStatusId { get;  set; }
    public DateTime CreatedAt { get;  set; }
    public long CreatedBy { get;  set; }


}
