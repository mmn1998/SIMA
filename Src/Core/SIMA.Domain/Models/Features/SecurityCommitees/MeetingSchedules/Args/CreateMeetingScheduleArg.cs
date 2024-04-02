namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Args;

public class CreateMeetingScheduleArg
{
    public long MeetingId { get; set; }
    public DateTime MeetingDateTime { get; set; }
    public string? Location { get; set; }
    /// <summary>
    /// TODO MeetingHoldingStatusId
    /// </summary>
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}