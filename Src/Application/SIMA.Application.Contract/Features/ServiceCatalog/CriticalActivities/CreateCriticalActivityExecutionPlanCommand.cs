namespace SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;

public class CreateCriticalActivityExecutionPlanCommand
{
    //public int WeekDayStart { get; set; }
    //public int WeekDayEnd { get; set; }
    public int WeekDay { get; set; }
    public string ServiceAvailabilityStartTime { get; set; } = string.Empty;
    public string ServiceAvailabilityEndTime { get; set; } = string.Empty;
}

