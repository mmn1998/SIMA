namespace SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Args;

public class ModifyMeetingScheduleArg
{
    public long MeetingId { get; set; }
    public DateTime MeetingDateTime { get; set; }
    public string? Location { get; set; }
    /// <summary>
    /// TODO MeetingHoldingStatusId
    /// </summary>
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
