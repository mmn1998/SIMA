using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueLink : Entity
{
    private IssueLink()
    {
    }

    public IssueLink(CreateIssueLinkArg arg)
    {
        Id = new IssueLinkId(IdHelper.GenerateUniqueId());
        IssueId = new IssueId(arg.IssueId);
        if (arg.IssueIdLinkedTo.HasValue) IssueIdLinkedTo = new(arg.IssueIdLinkedTo.Value);
        if (arg.IssueLinkReasonTo.HasValue) IssueIdLinkReasonTo = new(arg.IssueLinkReasonTo.Value);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueLink> Create(CreateIssueLinkArg arg)
    {
        return new IssueLink(arg);
    }
    public async Task Modify(ModifyIssueLinkArg arg)
    {
        IssueId = new IssueId(arg.IssueId);
        IssueIdLinkedTo = new(arg.IssueIdLinkedTo);
        IssueIdLinkReasonTo = new(arg.IssueLinkReasonTo);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueLinkId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public IssueId IssueIdLinkedTo { get; private set; }
    public Issue IssueLinkedTo { get; private set; }
    public IssueLinkReasonId IssueIdLinkReasonTo { get; private set; }
    public virtual IssueLinkReason IssueLinkedReason { get; private set; }
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
