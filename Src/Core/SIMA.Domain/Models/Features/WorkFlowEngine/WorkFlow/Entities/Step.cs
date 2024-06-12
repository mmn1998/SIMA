using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.StepApprovalOptions.Entities;
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
        if (arg.ActionTypeId.HasValue) ActionTypeId = new ActionTypeId((long)arg.ActionTypeId);
        //if (arg.StateId.HasValue) StateId = new StateId((long)arg.StateId);
        CompleteName = arg.CompleteName;
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
        //if (arg.StateId.HasValue)
        //{
        //    StateId = new StateId((long)arg.StateId);
        //}
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
        //StateId = new StateId((long)arg.StateId);
        //BpmnId = arg.BpmnId;
        CompleteName = arg.CompleteName;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        FormId = new FormId(arg.FormId);
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

    public async Task AddStepApprovalOption(List<CreateStepApprovalOptionArg> arg, long stepId)
    {
        var previousStepApprovalOptions = _stepApprovalOptions.Where(x => x.StepId == new StepId(stepId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addStepApprovalOption = arg.Where(x => !previousStepApprovalOptions.Any(c => c.ApprovalOptionId.Value == x.ApprovalOptionId)).ToList();
        var deleteStepApprovalOption = previousStepApprovalOptions.Where(x => !arg.Any(c => c.ApprovalOptionId == x.ApprovalOptionId.Value)).ToList();


        foreach (var item in addStepApprovalOption)
        {
            var entity = _stepApprovalOptions.Where(x => (x.ApprovalOptionId == new ApprovalOptionId(item.ApprovalOptionId) && x.StepId == new StepId(stepId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
               await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = StepApprovalOption.Create(item);
                _stepApprovalOptions.Add(entity);
            }
        }

        foreach (var item in deleteStepApprovalOption)
        {
            item.Delete();
        }
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Activate()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public StepId Id { get; private set; }
    public string? Name { get; private set; }
    public string? CompleteName { get; private set; }
    public WorkFlowId? WorkFlowId { get; private set; }
    public ActionTypeId? ActionTypeId { get; private set; }
    public FormId? FormId { get; private set; }
    public virtual Form? Form { get; private set; }
    public string? BpmnId { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

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
   
    private List<Issue> _issues = new();
    public ICollection<Issue> Issues => _issues;

    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;

    private List<IssueHistory> _sourceissueHistories = new();
    public ICollection<IssueHistory> SourceIssueHistories => _sourceissueHistories;
    private List<IssueHistory> _targetissueHistories = new();
    public ICollection<IssueHistory>? TargetIssueHistories => _targetissueHistories;

    private List<StepApprovalOption> _stepApprovalOptions = new();
    public ICollection<StepApprovalOption> StepApprovalOptions => _stepApprovalOptions;
    private List<StepOutputParam> _stepOutputParams = new();
    public ICollection<StepOutputParam> StepOutputParams => _stepOutputParams;
}
