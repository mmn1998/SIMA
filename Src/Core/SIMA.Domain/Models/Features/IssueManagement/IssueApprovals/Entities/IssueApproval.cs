using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
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
        ProductId = arg.ProductId;
        IsApproval = arg.IsApproval;
        WorkflowStepId = new(arg.WorkflowStepId);
        WorkflowActorId = new(arg.WorkflowActorId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public async Task<IssueApproval> Create(CreateIssueApprovalArg arg)
    {
        return new IssueApproval(arg);
    }
    public async Task Modify(ModifyIssueApprovalArg arg)
    {
        ProductId = arg.ProductId;
        IsApproval = arg.IsApproval;
        WorkflowStepId = new(arg.WorkflowStepId);
        WorkflowActorId = new(arg.WorkflowActorId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public IssueApprovalId Id { get; private set; }
    public long ProductId { get; private set; }
    public string IsApproval { get; private set; }
    public StepId WorkflowStepId { get; private set; }
    public virtual Step WorkflowStep { get; private set; }
    public WorkFlowActorId WorkflowActorId { get; private set; }
    public virtual WorkFlowActor WorkflowActor { get; private set; }
    public string Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
