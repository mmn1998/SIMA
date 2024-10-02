using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class StepApprovalOption : Entity, IAggregateRoot
{
    private StepApprovalOption()
    {
    }
    private StepApprovalOption(CreateStepApprovalOptionArg arg)
    {
        Id = new StepApprovalOptionId(IdHelper.GenerateUniqueId());
        StepId = new StepId(arg.StepId);
        ActiveStatusId = arg.ActiveStatusId;
        ApprovalOptionId = new ApprovalOptionId(arg.ApprovalOptionId);
        CreatedAt = DateTime.Now;
        CreatedBy = arg.CreatedBy;
    }
    public static StepApprovalOption Create(CreateStepApprovalOptionArg arg)
    {
        return new StepApprovalOption(arg);
    }

    public async Task Modify(ModifyStepApprovalOptionArg arg)
    {
        ApprovalOptionId = new ApprovalOptionId(arg.ApprovalOptionId);
        StepId = new StepId(arg.StepId);
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public async Task ChangeStatus(ActiveStatusEnum status)
    {
        ActiveStatusId = (long)status;
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public StepApprovalOptionId Id { get; private set; }
    public StepId StepId { get; private set; }
    public virtual Step Step { get; private set; }
    public ApprovalOptionId ApprovalOptionId { get; private set; }
    public virtual ApprovalOption ApprovalOption { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public long? ModifiedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }

    private List<IssueApproval> _issueApprovals = new();
    public ICollection<IssueApproval> IssueApprovals => _issueApprovals;
}
