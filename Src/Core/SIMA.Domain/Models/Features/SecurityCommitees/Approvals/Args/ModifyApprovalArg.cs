namespace SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Args;

public class ModifyApprovalArg
{
    public long Id { get; set; }
    public long MeetingId { get; set; }
    public long IssueId { get; set; }
    public DateTime DueDate { get; set; }
    public long ResponsibleComppanyId { get; set; }
    public long? ResponsibleDepartmentId { get; set; }
    public long? ResponsibleStaffId { get; set; }
    public long SupervisorComppanyId { get; set; }
    public long? SupervisorDepartmentId { get; set; }
    public long? SupervisorStaffId { get; set; }
    public string? IsPresented { get; set; }
    public string? IsSigned { get; set; }
    public string? IsArchived { get; set; }
    public long? ArchivedBy { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long ModifiedBy { get; set; }
}
