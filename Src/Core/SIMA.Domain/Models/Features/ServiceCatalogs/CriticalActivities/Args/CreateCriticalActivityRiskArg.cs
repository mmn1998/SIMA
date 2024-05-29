using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Args;

public class CreateCriticalActivityRiskArg 
{
    public long Id { get;  set; }
    public long CriticalActivityId { get;  set; }
    //? risk
    public long? ActiveStatusId { get;  set; }
    public DateTime? CreatedAt { get;  set; }
    public long? CreatedBy { get;  set; }
}
