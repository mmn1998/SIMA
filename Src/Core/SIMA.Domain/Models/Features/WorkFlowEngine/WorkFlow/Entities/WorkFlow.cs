using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Events;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Domain;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;

public class WorkFlow : Entity
{
    private WorkFlow()
    {
    }

    private WorkFlow(CreateWorkFlowArg arg)
    {
        Id = new WorkFlowId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        BpmnId = arg.BpmnId;
        FileContent = arg.FileContent;
        ProjectId = new ProjectId(arg.ProjectId);
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
        if (arg.ManagerRoleId.HasValue) ManagerRoleId = new(arg.ManagerRoleId.Value);
        ActiveStatusId = arg.ActiveStatusId;
        MainAggregateId = new(arg.MainAggregateId);
        Ordering = arg.Ordering;
        Description = arg.Description;

    }
    public static async Task<WorkFlow> New(CreateWorkFlowArg arg, IWorkFlowDomainService service)
    {
        await CreateGuards(arg, service);
        return new WorkFlow(arg);
    }
    public void AddSteps(IEnumerable<StepArg> args, long? modifiedUser)
    {
            var steps = args.Select(item => Step.New(item, modifiedUser));
            _step.AddRange(steps);
    }
    public void AddProgresses(IEnumerable<ProgressArg> args, long? modifiedUser)
    {
        var progresses = args.Select(item => Progress.Entities.Progress.New(item, modifiedUser));

        _progress.AddRange(progresses);
    }
    public void AddActors(IEnumerable<WorkFlowActorArg> args, long? userId)
    {
        var actors = args.Select(x => WorkFlowActor.Entites.WorkFlowActor.New(x , userId.Value));
        _workFlowActors.AddRange(actors);
    }
    public async Task Modify(ModifyWorkFlowArg arg, IWorkFlowDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        BpmnId = arg.BpmnId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        MainAggregateId = new(arg.MainAggregateId);
        Ordering = arg.Ordering;
        Description = arg.Description;
        ProjectId = new ProjectId(arg.ProjectId);
        Ordering = arg.Ordering;
        if (arg.ManagerRoleId.HasValue) ManagerRoleId = new(arg.ManagerRoleId.Value);
    }

    public void Modify(ModifyFileContentArg arg)
    {
        BpmnId = arg.BpmnId;
        //ModifiedAt = arg.ModifyAt;
        ModifiedBy = arg.ModifyBy;
        FileContent = arg.FileContent;
        AddSteps(arg.Steps, arg.ModifyBy);
        AddActors(arg.WorkFlowActors, arg.ModifyBy);
        AddProgresses(arg.Progresses, arg.ModifyBy);
    }
    public void ModifyFlow(ModifyFileContentArg arg)
    {
        BpmnId = arg.BpmnId;
        ModifiedBy = arg.ModifyBy;
        FileContent = arg.FileContent;
        ModifyActors(arg.WorkFlowActors, arg.ModifyBy);
        UpdateStepArg(arg.Steps, arg.WorkFlowActors);
        ModifySteps(arg.Steps, arg.ModifyBy);
        ModifyProgresses(arg.Progresses, arg.ModifyBy);
    }
    private void UpdateStepArg(List<StepArg> stepsArg, List<WorkFlowActorArg> actorsArg)
    {
        var actorSteps = stepsArg.SelectMany(x => x.ActorStepArgs);

        foreach (var item in actorSteps)
        {
            var actor = actorsArg.FirstOrDefault(x => x.BpmnId == item.ActorBpmnId);
            item.WorkFlowActorId = actor.LastId ?? actor.Id;
        }
    }
    private void ModifyProgresses(List<ProgressArg> args, long? modifiedBy)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveActors = _progress.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveActors)
        {
            item.Delete(modifiedBy.Value);
        }
        var existsBpmnIds = _progress.Select(x => x.BpmnId);
        var notExistsProgresses = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddProgresses(notExistsProgresses, modifiedBy);
        var existProgressArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existProgressArgs)
        {
            var progress = _progress.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            progress.Modify(item, modifiedBy);
        }
    }

    private void ModifyActors(List<WorkFlowActorArg> args, long? userId)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveActors = _workFlowActors.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveActors)
        {
            item.Delete(userId.Value);
        }
        var existsBpmnIds = _workFlowActors.Select(x => x.BpmnId);
        var notExistsActors = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddActors(notExistsActors, userId);
        var existActorArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existActorArgs)
        {
            var actor = _workFlowActors.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            if (actor is null)
                continue;
            item.LastId = actor.Id.Value;
            actor.Modify(item, userId.Value);
        }
    }

    private void ModifySteps(List<StepArg> args, long? userId)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveSteps = _step.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveSteps)
        {
            item.Delete(userId.Value);
        }
        var deactiveStepIds = deActiveSteps.Select(x => x.Id).ToList();
        AddDomainEvent(new WorkflowModifiedEvent(deactiveStepIds, userId.Value));
        var existsBpmnIds = _step.Select(x => x.BpmnId);
        var notExistsSteps = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddSteps(notExistsSteps, userId);
        var existActorArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existActorArgs)
        {
            var actor = _step.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            actor.Modify(item, userId.Value);
        }
    }

    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        #region DeactiveRealtedEntities

        foreach (var step in _step)
        {
            step.Delete(userId);
        }
        foreach (var state in _states)
        {
            state.Delete(userId);
        }
        foreach (var workflowCompany in _workFlowCompany)
        {
            workflowCompany.Delete(userId);
        }
        foreach (var progress in _progress)
        {
            progress.Delete(userId);
        }
        foreach (var workflowActor in _workFlowActors)
        {
            workflowActor.Delete(userId);
        }
        var stepIds = _step.Select(x => x.Id).ToList();
        AddDomainEvent(new WorkflowModifiedEvent(stepIds, userId));

        #endregion
    }
    public void Activate(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        #region ActivateRealtedEntities

        foreach (var step in _step)
        {
            step.Activate(userId);
        }
        foreach (var state in _states)
        {
            state.Activate(userId);
        }
        foreach (var workflowCompany in _workFlowCompany)
        {
            workflowCompany.Activate(userId);
        }
        foreach (var progress in _progress)
        {
            progress.Activate(userId);
        }
        foreach (var workflowActor in _workFlowActors)
        {
            workflowActor.Activate(userId);
        }
        var stepIds = _step.Select(x => x.Id).ToList();

        AddDomainEvent(new WorkflowModifiedEvent(stepIds, userId));

        #endregion
    }
    public bool DeleteStep(long stepId, long userId)
    {
        var result = _step.Where(x => x.Id == new StepId(stepId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(userId);
            return true;
        }
        else
            return false;
    }
    public bool DeleteState(long stateId, long userId)
    {
        var result = _states.Where(x => x.Id == new StateId(stateId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete(userId);
            return true;
        }
        else
            return false;
    }
    public void ModifyStep(ModifyStepArgs arg, long userId)
    {
        var result = _step.Where(x => x.Id == new StepId(arg.Id)).FirstOrDefault();
        if (result is not null)
        {
            result.Modify(arg, userId);
        }
    }

    public async Task AddStepApproval(List<CreateStepApprovalOptionArg> args, long StepId, IWorkFlowDomainService service)
    {
        var checkApproval = await service.AllowAddApprovalForStep(StepId);
        if (checkApproval)
        {
            var step = _step.Where(x => x.Id == new StepId(StepId)).FirstOrDefault();
            step.AddStepApprovalOption(args, StepId);
        }
        else
            throw new SimaResultException(CodeMessges._400Code, Messages.StepNotAllowedAddApproval);

    }
    public async Task AddRequirmentDocument(List<CreateStepRequiredDocumentArg> args, long StepId)
    {
        var step = _step.Where(x => x.Id == new StepId(StepId)).FirstOrDefault();
        await step.AddStepRequirmentDocument(args, StepId);
    }
    public async Task ModifyState(ModifyStateArgs arg, IWorkFlowDomainService service)
    {
        var result = _states.Where(x => x.Id == new StateId(arg.Id)).FirstOrDefault();
        if (result is not null)
        {
            await result.Modify(arg, service);
        }
    }
    public Step AddStep(StepArg arg, long userId)
    {
        var step = Step.New(arg, userId);
        _step.Add(step);
        return step;

    }
    public async Task<State> AddState(CreateStateArg arg, IWorkFlowDomainService service)
    {
        var state = await State.New(arg, service);
        _states.Add(state);
        return state;

    }



    private static async Task CreateGuards(CreateWorkFlowArg arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.WorkFlowNameRequiredException);

    }

    private async Task ModifyGuards(ModifyWorkFlowArg arg, IWorkFlowDomainService service)
    {
        if (arg.ActiveStatusId == (long)ActiveStatusEnum.Active && ActiveStatusId != (long)ActiveStatusEnum.Active)
        {
            Activate(Id.Value);
        }
        arg.Name.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.WorkFlowCodeIsUniqueException);

    }

    public WorkFlowId Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public ProjectId ProjectId { get; set; }
    public string? FileContent { get; set; }
    public MainAggregateId? MainAggregateId { get; set; }
    public virtual MainAggregate? MainAggregate { get; set; }
    public RoleId ManagerRoleId { get; set; }
    public virtual Role ManagerRole { get; set; }
    public string? BpmnId { get; set; }
    public float? Ordering { get; set; }
    public string? Description { get; set; }
    public long? ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    //public virtual ICollection<Action> Actions { get; set; } = new List<Action>();
    public virtual Project.Entites.Project Project { get; set; } = null!;

    private List<State> _states = new();
    public List<State> States => _states;
    private List<WorkFlowActor.Entites.WorkFlowActor> _workFlowActors = new();
    public List<WorkFlowActor.Entites.WorkFlowActor> WorkFlowActors => _workFlowActors;
    private List<Step> _step = new();
    public List<Step> Steps => _step;
    private List<Progress.Entities.Progress> _progress = new();
    public virtual ICollection<Progress.Entities.Progress> Progresses => _progress;


    private List<WorkFlowCompany.Entities.WorkFlowCompany> _workFlowCompany = new();
    public virtual ICollection<WorkFlowCompany.Entities.WorkFlowCompany> WorkFlowCompanies => _workFlowCompany;
    //private List<Issue> _Issues = new();
    //public virtual ICollection<Issue> Issues => _Issues;
    //private List<IssueChangeHistory> _issueChangeHistories = new();
    //public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
}
