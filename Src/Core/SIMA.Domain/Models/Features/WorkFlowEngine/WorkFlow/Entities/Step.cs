using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class Step : Entity
{
    private Step()
    {

    }
    private Step(StepArg arg, long? createdBy)
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
        CreatedBy = createdBy;
        CreatedAt = arg.CreatedAt;
        AddActorStep(arg.ActorStepArgs);

    }

    public void Modify(StepArg arg, long createdBy)
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
        UIPropertyBoxTitle = arg.UIPropertyBoxTitle;
        ModifiedBy = createdBy;
        IsAssigneeForced = arg.IsAssigneeForced;
        var allBmpnIds = arg.ActorStepArgs.Select(x => x.BpmnId);

        var deActiveSteps = _workFlowActorStep.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveSteps)
        {
            item.Delete(createdBy);
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
    public void Modify(ModifyStepArgs arg, long? createdBy)
    {
        //Name = arg.Name;
        WorkFlowId = new WorkFlowId((long)arg.WorkFlowId);
        CompleteName = arg.CompleteName;
        HasDocument = arg.HasDocument;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        DisplayName =arg.DisplayName;
        FormId = new FormId(arg.FormId).Value != 0 ? new FormId(arg.FormId) : null;
    }
    public static Step New(StepArg arg, long? createdBy)
    {
        return new Step(arg, createdBy);
    }
    public void AddActorStep(IEnumerable<CreateWorkFlowActorStepArg> actorSteps)
    {
        var actorStep = actorSteps.Select(x => WorkFlowActorStep.New(x));
        _workFlowActorStep.AddRange(actorStep);
    }

    public void AddStepApprovalOption(List<CreateStepApprovalOptionArg> arg, long stepId)
    {
        var previousStepApprovalOptions = _stepApprovalOptions.Where(x => x.StepId == new StepId(stepId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

        var addStepApprovalOption = arg.Where(x => !previousStepApprovalOptions.Any(c => c.ApprovalOptionId.Value == x.ApprovalOptionId)).ToList();
        var deleteStepApprovalOption = previousStepApprovalOptions.Where(x => !arg.Any(c => c.ApprovalOptionId == x.ApprovalOptionId.Value)).ToList();


        foreach (var item in addStepApprovalOption)
        {
            var entity = _stepApprovalOptions.Where(x => (x.ApprovalOptionId == new ApprovalOptionId(item.ApprovalOptionId) && x.StepId == new StepId(stepId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = StepApprovalOption.Create(item);
                _stepApprovalOptions.Add(entity);
            }
        }

        foreach (var item in deleteStepApprovalOption)
        {
            item.Delete(arg.First().CreatedBy);
        }
    }
    public async Task AddStepRequirmentDocument(List<CreateStepRequiredDocumentArg> arg, long stepId)
    {
        var previousDocuments = _stepRequiredDocuments.Where(x => x.StepId == new StepId(stepId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active).ToList();

        var changeCount = arg.Where(x => previousDocuments.Any(c => c.DocumentTypeId.Value == x.DocumentTypeId && c.Count != x.Count)).ToList();
        var addStepDocuments = arg.Where(x => !previousDocuments.Any(c => c.DocumentTypeId.Value == x.DocumentTypeId)).ToList();
        var deleteStepDocuments = previousDocuments.Where(x => !arg.Any(c => c.DocumentTypeId == x.DocumentTypeId.Value)).ToList();

        foreach (var item in changeCount)
        {
            var entity = _stepRequiredDocuments.Where(x => x.DocumentTypeId.Value == item.DocumentTypeId).FirstOrDefault();
            await entity.Modify(item);
        }

        foreach (var item in addStepDocuments)
        {
            var entity = _stepRequiredDocuments.Where(x => (x.DocumentTypeId == new DocumentTypeId(item.DocumentTypeId) && x.StepId == new StepId(stepId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
            if (entity is not null)
            {
                await entity.ChangeStatus(ActiveStatusEnum.Active);
            }
            else
            {
                entity = StepRequiredDocument.Create(item);
                _stepRequiredDocuments.Add(entity);
            }
        }

        foreach (var item in deleteStepDocuments)
        {
            item.Delete((arg.First().CreatedBy).Value);
        }
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    public void Activate(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
    public StepId Id { get; private set; }
    public string? IsAssigneeForced { get; private set; }
    public string? Name { get; private set; }
    public string? CompleteName { get; private set; }
    public string? HasDocument { get; private set; }
    public string? DisplayName { get; private set; }
    public string? UIPropertyBoxTitle { get; private set; }
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

    private List<StepRequiredDocument> _stepRequiredDocuments = new();
    public ICollection<StepRequiredDocument> StepRequiredDocuments => _stepRequiredDocuments;

    private List<StepServiceTask> _stepServiceTasks = new();
    public ICollection<StepServiceTask> StepServiceTasks => _stepServiceTasks;
}
