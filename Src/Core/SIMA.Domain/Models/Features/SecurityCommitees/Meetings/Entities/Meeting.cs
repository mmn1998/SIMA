using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;

public class Meeting : Entity
{
    private Meeting() { }
    private Meeting(CreateMeetingArg arg, List<CreateLabelArg> labels)
    {
        Id = new MeetingId(arg.Id);
        Code = arg.Code;
        MeetingTurn = arg.MeetingTurn;
        IssueId = new IssueId(arg.IssueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        Description = arg.Description;
        //CreateLable(labels);
        CreateMeetingLable(labels);

        AddDomainEvent(new MeetingCreatedEvent(IssueId.Value, MainAggregateEnums.SecurityCommitee, arg.Description, arg.Id, labels.Where(x => x.IsNew), arg.NewSubject));
    }
    public static async Task<Meeting> Create(CreateMeetingArg arg, IMeetingDomainService service)
    {
        await CreateGuards(arg, service);
        var label = await service.GetLabels(arg.Labels);
        return new Meeting(arg, label);
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

    public void CreateLable(List<CreateLabelArg> lables)
    {
        var meetingLabelsArg = lables.Select(x => MeetingLabel.Create(Id.Value, x.Id));
        _meetingLabels.AddRange(meetingLabelsArg);
    }
    public void CreateMeetingLable(List<CreateLabelArg> lables)
    {
        //sanaz
        var meetingLabel = lables.Select(it => MeetingLabel.Create(new CreateMeetingLabelArg { MeetingId = Id.Value, LabelId = it.Id, CreatedBy = it.CreatedBy, }));
        _meetingLabels.AddRange(meetingLabel);
    }

    public void CreateMeetingLabel(IEnumerable<CreateMeetingLabelArg> args)
    {
        var meetingLabel = args.Select(MeetingLabel.Create);
        _meetingLabels.AddRange(meetingLabel);
    }

    public void CreateMeetingDocument(IEnumerable<CreateMeetingDocumentArg> args)
    {
        var meetingDocument = args.Select(MeetingDocument.Create);
        _meetingDocuments.AddRange(meetingDocument);
    }

    public void CreateMeetingReason(IEnumerable<CreateMeetingReasonArg> args)
    {
        //sanaz
        var meetingReason = args.Select(MeetingReason.Create);
        _meetingReasons.AddRange(meetingReason);
    }

    public static async Task<List<Subject>> CreateSubject(List<CreateSubjectArg> subjects, IMeetingDomainService service)
    {
        var subjectList = new List<Subject>();
        foreach (var subject in subjects)
        {

            var subjectEntity = await Subject.Create(subject);
        }

        return subjectList;
    }

    public void CreateMeetingSubject(IEnumerable<CreateSubjectMeetingArg> args)
    {
        var meetingSubject = args.Select(SubjectMeeting.Create);
        _subjectMeetings.AddRange(meetingSubject);
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

    private List<Approval> _approvals = new();
    public ICollection<Approval> Approvals => _approvals;

    private List<MeetingDocument> _meetingDocuments = new();
    public virtual ICollection<MeetingDocument> MeetingDocuments => _meetingDocuments;

    private List<MeetingReason> _meetingReasons = new();
    public virtual ICollection<MeetingReason> MeetingReasons => _meetingReasons;

    private List<MeetingSchedule> _meetingSchedules = new();
    public ICollection<MeetingSchedule> MeetingSchedules => _meetingSchedules;

    private List<SubjectMeeting> _subjectMeetings = new();
    public virtual ICollection<SubjectMeeting> SubjectMeetings => _subjectMeetings;

    private List<MeetingLabel> _meetingLabels = new();
    public virtual ICollection<MeetingLabel> MeetingLabels => _meetingLabels;

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Activate(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
