using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Interfaces;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.ValueObjects;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Domain;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;

public class SubjectPriority : Entity
{
    private SubjectPriority() { }
    private SubjectPriority(CreateSubjectPriorityArg arg)
    {
        Id = new(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
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
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateSubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, 0))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifySubjectPriorityArg arg, ISubjectPriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (arg.Name.Length >= 200)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);

        if (arg.Code.Length >= 20)
            throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);

        if (await service.IsCodeUnique(arg.Code, arg.Id))
            throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public SubjectPriorityId Id{ get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
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
