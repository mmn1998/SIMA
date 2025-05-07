using SIMA.Framework.Common.Helper;


namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityExecutionPlansQueryResult
{
    public int WeekDay { get; set; }
    public TimeSpan? ServiceAvalibilityStartTime { get; set; }
    private TimeOnly? ServiceAvalibilityStartTimeOnly
    {
        get
        {
            return ServiceAvalibilityStartTime.HasValue
                ? TimeOnly.FromTimeSpan(ServiceAvalibilityStartTime.Value)
                : (TimeOnly?)null;
        }
    }
    public string? ServiceAvalibilityStartTimeNormalized => DateHelper.ToTimeOnly(ServiceAvalibilityStartTimeOnly);

    public TimeSpan? ServiceAvalibilityEndTime { get; set; }
    private TimeOnly? ServiceAvalibilityEndTimeOnly
    {
        get
        {
            return ServiceAvalibilityEndTime.HasValue
                ? TimeOnly.FromTimeSpan(ServiceAvalibilityEndTime.Value)
                : (TimeOnly?)null;
        }
    }
    public string? ServiceAvalibilityEndTimeNormalized => DateHelper.ToTimeOnly(ServiceAvalibilityEndTimeOnly);
}