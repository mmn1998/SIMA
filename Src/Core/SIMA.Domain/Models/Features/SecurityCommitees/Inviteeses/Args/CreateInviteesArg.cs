namespace SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Args;

public class CreateInviteesArg
{
    public long MeetingScheduleId { get; set; }
    public long ComppanyId { get; set; }
    public long? DepartmentId { get; set; }
    public long? StaffId { get; set; }
    public string? IsCompanyRepresentative { get; set; }
    public string? IsPresented { get; set; }
    public string? IsSigned { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}