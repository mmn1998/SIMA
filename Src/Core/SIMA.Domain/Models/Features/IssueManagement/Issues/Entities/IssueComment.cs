using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueComment : Entity
{
    private IssueComment()
    {
    }

    public IssueComment(CreateIssueCommentArg arg)
    {
        Id = new IssueCommentId(IdHelper.GenerateUniqueId());
        IssueId = new IssueId(arg.IssueId);
        Comment = arg.Comment;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueComment> Create(CreateIssueCommentArg arg)
    {
        await CreateGuards(arg);
        return new IssueComment(arg);
    }
    private static async Task CreateGuards(CreateIssueCommentArg arg)
    {
        arg.NullCheck();
        arg.Comment.NullCheck();
    }
    public async Task Modify(ModifyIssueCommentArg arg)
    {
        IssueId = new IssueId(arg.IssueId);
        Comment = arg.Comment;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueCommentId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public string Comment { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
