using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.Services.Args;

public class CreateServiceAvalibilityArg
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public int WeekDay { get; set; }
    public TimeOnly ServiceAvalibilityStartTime { get; set; }
    public TimeOnly ServiceAvalibilityEndTime { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
}
