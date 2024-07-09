using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Args;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Interfaces;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Runtime.CompilerServices;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;

public class IssuePriority : Entity

{
    private IssuePriority()
    {
    }

    public IssuePriority(CreateIssuePriorityArg arg)
    {
        Id = new IssuePriorityId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssuePriority> Create(CreateIssuePriorityArg arg, IIssuePriorityDomainService service)
    {
        await CreateGuard(arg, service);
        return new IssuePriority(arg);
    }
    public async Task Modify(ModifyIssuePriorityArg arg, IIssuePriorityDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuard(CreateIssuePriorityArg arg, IIssuePriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifyIssuePriorityArg arg, IIssuePriorityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code,Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code,Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public IssuePriorityId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public float Ordering { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;
    }
}
