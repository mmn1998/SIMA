using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;

public class IssueHistory : Entity
{
    private IssueHistory()
    {
    }

    private IssueHistory(CreateIssueHistoryArg arg)
    {
        Id = new IssueHistoryId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        IssueId = new IssueId(arg.IssueId);
        SourceStateId = arg.SourceStateId;
        SourceStepId = arg.SourceStepId;
        TargetStateId = arg.TargetStateId;
        TargetStepId = arg.TargetStepId;
        PerformerUserId = arg.PerformerUserId;
        Description = arg.Description;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static IssueHistory Create(CreateIssueHistoryArg arg)
    {
        return new IssueHistory(arg);
    }

    public IssueHistoryId Id { get; private set; }
    public string Name { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public long SourceStateId { get; private set; }
    public long TargetStateId { get; private set; }
    public long SourceStepId { get; private set; }
    public long TargetStepId { get; private set; }
    public long PerformerUserId { get; private set; }
    public string Description { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
}
