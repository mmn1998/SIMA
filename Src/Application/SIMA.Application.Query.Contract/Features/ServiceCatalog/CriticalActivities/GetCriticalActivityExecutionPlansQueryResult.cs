using SIMA.Framework.Common.Helper;


namespace SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;

public class GetCriticalActivityExecutionPlansQueryResult
{
    public int WeekDay { get; set; }
    public TimeOnly? ServiceAvalibilityStartTime { get; set; }
    public string? ServiceAvalibilityStartTimeNormalized => DateHelper.ToTimeOnly(ServiceAvalibilityStartTime);
    public TimeOnly? ServiceAvalibilityEndTime { get; set; }
    public string? ServiceAvalibilityEndTimeNormalized => DateHelper.ToTimeOnly(ServiceAvalibilityEndTime);
}