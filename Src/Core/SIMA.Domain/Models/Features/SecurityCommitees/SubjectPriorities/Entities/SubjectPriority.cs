using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;

public class SubjectPriority
{
    private SubjectPriority() { }
    private SubjectPriority(CreateSubjectPriorityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Title = arg.Title;
        Description = arg.Description;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<SubjectPriority> Create(CreateSubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {
        await CreateGuards(arg, service);
        return new SubjectPriority(arg);
    }
    public async Task Modify(ModifySubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {
        await ModifyGuards(arg, service);
        Title = arg.Title;
        Description = arg.Description;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateSubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {

    }
    private async Task ModifyGuards(ModifySubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {

    }
    #endregion
    public SubjectPriorityId Id{ get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public float? Ordering { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<SubjectMeeting> _subjectMeetings => new();
    public ICollection<SubjectMeeting> SubjectMeetings => _subjectMeetings;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
