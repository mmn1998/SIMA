using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.StepApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;

public class IssueApproval : Entity
{
    private IssueApproval()
    {
    }

    public IssueApproval(CreateIssueApprovalArg arg)
    {
        Id = new IssueApprovalId(IdHelper.GenerateUniqueId());
        IssueId = new IssueId(arg.IssueId);
        StepApprovalOptionId = new StepApprovalOptionId(arg.StepApprovalOptionId);
        ApprovedBy = arg.ApprovedBy;
        WorkflowActorId = new WorkFlowActorId(arg.WorkflowActorId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<IssueApproval> Create(CreateIssueApprovalArg arg)
    {
        return new IssueApproval(arg);
    }
    public async Task Modify(ModifyIssueApprovalArg arg)
    {
        IssueId = new IssueId(arg.IssueId);
        StepApprovalOptionId = new StepApprovalOptionId(arg.StepApprovalOptionId);
        ApprovedBy = arg.ApprovedBy;
        WorkflowActorId = new WorkFlowActorId(arg.WorkflowActorId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public IssueApprovalId Id { get; private set; }
    public IssueId IssueId { get; private set; }
    public virtual Issue Issue { get; private set; }
    public StepApprovalOptionId StepApprovalOptionId { get; private set; }
    public virtual StepApprovalOption StepApprovalOption { get; private set; }
    public long ApprovedBy { get;private set; }
    public WorkFlowActorId WorkflowActorId { get; private set; }
    public virtual WorkFlowActor WorkflowActor { get; private set; }
    public string Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
