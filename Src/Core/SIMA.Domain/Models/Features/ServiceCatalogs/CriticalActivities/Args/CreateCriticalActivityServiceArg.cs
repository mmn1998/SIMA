using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityServiceArg 
{
    public long Id { get;  set; }
      public long CriticalActivityId { get;  set; }
    public long ServiceId { get;  set; }
    public long? ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
