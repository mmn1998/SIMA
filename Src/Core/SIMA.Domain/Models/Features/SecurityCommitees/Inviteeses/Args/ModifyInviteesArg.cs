namespace SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Args;

public class ModifyInviteesArg
{
    public long MeetingScheduleId { get; set; }
    public long ComppanyId { get; set; }
    public long? DepartmentId { get; set; }
    public long? StaffId { get; set; }
    public string? IsCompanyRepresentative { get; set; }
    public string? IsPresented { get; set; }
    public string? IsSigned { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
