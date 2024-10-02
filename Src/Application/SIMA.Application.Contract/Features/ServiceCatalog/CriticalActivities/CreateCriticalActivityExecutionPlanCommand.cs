namespace SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;

public class CreateCriticalActivityExecutionPlanCommand
{
    public int WeekDay { get; set; }
    public string ServiceAvalibilityStartTime { get; set; } = string.Empty;
    public string ServiceAvalibilityEndTime { get; set; } = string.Empty;
}

