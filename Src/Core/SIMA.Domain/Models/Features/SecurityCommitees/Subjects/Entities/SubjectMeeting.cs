using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;

public class SubjectMeeting
{
    private SubjectMeeting() { }
    private SubjectMeeting(CreateSubjectMeetingArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        if (arg.SubjectId.HasValue) SubjectId = new(arg.SubjectId.Value);
        if (arg.MeetingId.HasValue) MeetingId = new(arg.MeetingId.Value);
        if (arg.SubjectPriorityId.HasValue) SubjectPriorityId = new(arg.SubjectPriorityId.Value);
        IsOutOfOrder = arg.IsOutOfOrder;
        IsConfirmed = arg.IsConfirmed;
        if (arg.ConfirmedBy.HasValue) ConfirmedBy = new(arg.ConfirmedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<SubjectMeeting> Create(CreateSubjectMeetingArg arg, ISubjectMeetingDomainService service)
    {
        await CreateGuards(arg, service);
        return new SubjectMeeting(arg);
    }
    public async Task Modify(ModifySubjectMeetingArg arg, ISubjectMeetingDomainService service)
    {
        if (arg.SubjectId.HasValue) SubjectId = new(arg.SubjectId.Value);
        if (arg.MeetingId.HasValue) MeetingId = new(arg.MeetingId.Value);
        if (arg.SubjectPriorityId.HasValue) SubjectPriorityId = new(arg.SubjectPriorityId.Value);
        IsOutOfOrder = arg.IsOutOfOrder;
        IsConfirmed = arg.IsConfirmed;
        if (arg.ConfirmedBy.HasValue) ConfirmedBy = new(arg.ConfirmedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        await ModifyGuards(arg, service);
    }
    #region Guards
    private static async Task CreateGuards(CreateSubjectMeetingArg arg, ISubjectMeetingDomainService service)
    {

    }
    private async Task ModifyGuards(ModifySubjectMeetingArg arg, ISubjectMeetingDomainService service)
    {

    }
    #endregion
    public SubjectMeetingId Id { get; private set; }
    public SubjectId SubjectId { get; private set; }
    public virtual Subject Subject { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public virtual Meeting Meeting { get; private set; }
    public SubjectPriorityId? SubjectPriorityId { get; private set; }
    public virtual SubjectPriority? SubjectPriority { get; private set; }
    public string? IsOutOfOrder { get; private set; }
    public string? IsConfirmed { get; private set; }
    public UserId? ConfirmedBy { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
