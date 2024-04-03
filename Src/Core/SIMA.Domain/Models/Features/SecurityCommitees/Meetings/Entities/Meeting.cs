using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;

public class Meeting
{
    private Meeting() { }
    private Meeting(CreateMeetingArg arg) 
    {
        Id = new(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        MeetingTurn = arg.MeetingTurn;
        if (arg.IssueId.HasValue) IssueId = new(arg.IssueId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Meeting> Create(CreateMeetingArg arg, IMeetingDomainService service)
    {
        await CreateGuards(arg, service);
        return new Meeting(arg);
    }
    public async Task Modify(ModifyMeetingArg arg, IMeetingDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        MeetingTurn = arg.MeetingTurn;
        if (arg.IssueId.HasValue) IssueId = new(arg.IssueId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateMeetingArg arg, IMeetingDomainService service)
    {

    }
    private static async Task ModifyGuards(ModifyMeetingArg arg, IMeetingDomainService service)
    {

    }

    #endregion
    public MeetingId Id { get; private set; }
    public string? Code { get; private set; }
    public int? MeetingTurn { get; private set; }
    public string? Description { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<Approval> _approvals => new();
    public ICollection<Approval> Approvals => _approvals;
    private List<MeetingDocument> _meetingDocuments => new();
    public ICollection<MeetingDocument> MeetingDocuments => _meetingDocuments;
    private List<MeetingReason> _meetingReasons => new();
    public ICollection<MeetingReason> MeetingReasons => _meetingReasons;
    private List<MeetingSchedule> _meetingSchedules => new();
    public ICollection<MeetingSchedule> MeetingSchedules => _meetingSchedules;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
