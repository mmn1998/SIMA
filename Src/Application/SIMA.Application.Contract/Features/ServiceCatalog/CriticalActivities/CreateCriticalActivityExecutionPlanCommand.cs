namespace SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;

public class CreateCriticalActivityExecutionPlanCommand
{
    public int WeekDayStart { get; set; }
    public int WeekDayEnd { get; set; }
    public string ServiceAvalibilityStartTime { get; set; } = string.Empty;
    public string ServiceAvalibilityEndTime { get; set; } = string.Empty;
}

