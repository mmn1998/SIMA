using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;

public class Subject
{
    private Subject() { }
    private Subject(CreateSubjectArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Title = arg.Title;
        Description = arg.Description;
        IsArchived = arg.IsArchived;
        if (arg.ArchivedBy.HasValue) ArchivedBy = new(arg.ArchivedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Subject> Create(CreateSubjectArg arg, ISubjectDomainService service)
    {
        await CreateGuards(arg, service);
        return new Subject(arg);
    }
    public async Task Modify(ModifySubjectArg arg, ISubjectDomainService service)
    {
        await ModifyGuards(arg, service);
        Title = arg.Title;
        Description = arg.Description;
        IsArchived = arg.IsArchived;
        if (arg.ArchivedBy.HasValue) ArchivedBy = new(arg.ArchivedBy.Value);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateSubjectArg arg, ISubjectDomainService service)
    {

    }
    private static async Task ModifyGuards(ModifySubjectArg arg, ISubjectDomainService service)
    {

    }
    #endregion
    public SubjectId Id { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public string? IsArchived { get; private set; }
    public UserId? ArchivedBy { get; private set; }
    public virtual User? Archiver { get; private set; }
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
