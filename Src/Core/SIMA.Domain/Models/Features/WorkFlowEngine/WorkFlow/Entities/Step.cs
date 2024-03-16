using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class Step : Entity
{
    private Step()
    {

    }
    private Step(StepArg arg)
    {
        Id = new StepId(arg.Id);
        Name = arg.Name ?? "";
        //TODO sanaz should check ActionTypeId
        //WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
         if(arg.ActionTypeId.HasValue) ActionTypeId = new ActionTypeId((long)arg.ActionTypeId);
        if (arg.StateId.HasValue) StateId = new StateId((long)arg.StateId);
        BpmnId = arg.BpmnId;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.UserId;
        CreatedAt = arg.CreatedAt;
    }

    public void Modify(StepArg arg)
    {
        Name = arg.Name;
        ActiveStatusId = arg.ActiveStatusId;
        if (arg.ActionTypeId.HasValue)
        {
            ActionTypeId = new ActionTypeId((long)arg.ActionTypeId);
        }
        if (arg.StateId.HasValue)
        {
            StateId = new StateId((long)arg.StateId);
        }
        ModifiedBy = arg.UserId;
        var allBmpnIds = arg.ActorStepArgs.Select(x => x.BpmnId);

        var deActiveSteps = _workFlowActorStep.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveSteps)
        {
            item.Delete();
        }
        var existsBpmnIds = _workFlowActorStep.Select(x => x.BpmnId);
        var notExistsSteps = arg.ActorStepArgs.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddActorStep(notExistsSteps);
        var existActorArgs = arg.ActorStepArgs.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existActorArgs)
        {
            var actor = _workFlowActorStep.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            actor.Modify(item);
        }

    }
    public void Modify(ModifyStepArgs arg)
    {
        Name = arg.Name;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        //ActionTypeId = new ActionTypeId((long)arg.ActionTypeId);
        StateId = new StateId((long)arg.StateId);
        //BpmnId = arg.BpmnId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public static Step New(StepArg arg)
    {
        return new Step(arg);
    }
    public void AddActorStep(IEnumerable<CreateWorkFlowActorStepArg> actorSteps)
    {
        var actorStep = actorSteps.Select(x => WorkFlowActorStep.New(x));
        _workFlowActorStep.AddRange(actorStep);
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    public StepId Id { get; set; }
    public string? Name { get; set; }
    public WorkFlowId? WorkFlowId { get; set; }
    public ActionTypeId? ActionTypeId { get; set; }
  //  public FormId? FormId { get; set; }
    public StateId? StateId { get; set; }
    public string? BpmnId { get; private set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

    //public virtual Form.Entities.Form? Form { get; set; }
    //private List<Form.Entities.Form> _form = new();
    public virtual State? State { get; set; }
    private List<Progress.Entities.Progress> _sourceProgresses = new();
    public List<Progress.Entities.Progress> SourceProgresses => _sourceProgresses;
    private List<Progress.Entities.Progress> _targetProgresses = new();
    public List<Progress.Entities.Progress> TargetProgresses => _targetProgresses;
    public virtual ActionType.Entites.ActionType? ActionType { get; set; }
    public virtual WorkFlow? WorkFlow { get; set; }
    private List<WorkFlowActorStep> _workFlowActorStep = new();
    public ICollection<WorkFlowActorStep> WorkFlowActorSteps => _workFlowActorStep;
    private List<Document> _documents = new();
    public ICollection<Document> Documents => _documents;
    private List<IssueApproval> _issueApprovals = new();
    public ICollection<IssueApproval> IssueApprovals => _issueApprovals;
    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;
    private List<IssueHistory> _sourceissueHistories = new();
    public ICollection<IssueHistory> SourceIssueHistories => _sourceissueHistories;
    private List<IssueHistory> _targetissueHistories = new();
    public ICollection<IssueHistory>? TargetIssueHistories => _targetissueHistories;
}
