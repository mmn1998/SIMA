using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
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
        SourceStateId = arg.SourceStateId== null ? null:new(arg.SourceStateId.Value);
        SourceStepId = new(arg.SourceStepId);
        if(arg.TargetStateId.HasValue && arg.TargetStateId != 0) TargetStateId = new(arg.TargetStateId.Value);
        if(arg.TargetStepId.HasValue) TargetStepId = new(arg.TargetStepId.Value);
        PerformerUserId = new(arg.PerformerUserId);
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
    public StateId? SourceStateId { get; private set; }
    public virtual State SourceState { get; private set; }
    public StateId? TargetStateId { get; private set; }
    public State? TargetState { get; private set; }
    public StepId SourceStepId { get; private set; }
    public virtual Step SourceStep { get; private set; }
    public StepId? TargetStepId { get; private set; }
    public virtual Step? TargetStep { get; private set; }
    public UserId PerformerUserId { get; private set; }
    public virtual User PerformerUser { get; private set; }
    public string Description { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
}
