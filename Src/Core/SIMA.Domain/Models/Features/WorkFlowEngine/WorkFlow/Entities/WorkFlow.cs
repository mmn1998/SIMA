using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.ValueObjects;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Exceptions;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

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

    }
    public static async Task<WorkFlow> New(CreateWorkFlowArg arg , IWorkFlowDomainService service)
    {
        await CreateGuards(arg, service);
        return new WorkFlow(arg);
    }
    public void AddSteps(IEnumerable<StepArg> args)
    {
        foreach (var item in args)
        {
            var step = Step.New(item);
            step.AddActorStep(item.ActorStepArgs);
            _step.Add(step);
        }
    }
    public void AddProgresses(IEnumerable<ProgressArg> args)
    {
        var progresses = args.Select(Progress.Entities.Progress.New);

        _progress.AddRange(progresses);
    }
    public void AddActors(IEnumerable<WorkFlowActorArg> args)
    {
        var actors = args.Select(WorkFlowActor.Entites.WorkFlowActor.New);
        _workFlowActors.AddRange(actors);
    }
    public async Task Modify(ModifyWorkFlowArg arg , IWorkFlowDomainService service)
    {
        await ModifyGuards(arg, service);
        Code = arg.Code;
        Name = arg.Name;
        BpmnId = arg.BpmnId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        MainAggregateId = new(arg.MainAggregateId);
        Ordering = arg.Ordering;
    }

    public async Task Modify(ModifyFileContentArg arg)
    {
        BpmnId = arg.BpmnId;
        //ModifiedAt = arg.ModifyAt;
        ModifiedBy = arg.ModifyBy;
        FileContent = arg.FileContent;
        AddSteps(arg.Steps);
        AddActors(arg.WorkFlowActors);
        AddProgresses(arg.Progresses);
    }
    public void ModifyFlow(ModifyFileContentArg arg)
    {
        BpmnId = arg.BpmnId;
        ModifiedBy = arg.ModifyBy;
        FileContent = arg.FileContent;
        ModifySteps(arg.Steps);
        ModifyActors(arg.WorkFlowActors);
        ModifyProgresses(arg.Progresses);
    }

    private void ModifyProgresses(List<ProgressArg> args)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveActors = _progress.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveActors)
        {
            item.Delete();
        }
        var existsBpmnIds = _progress.Select(x => x.BpmnId);
        var notExistsProgresses = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddProgresses(notExistsProgresses);
        var existProgressArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existProgressArgs)
        {
            var progress = _progress.FirstOrDefault(x => x.BpmnId ==  item.BpmnId);
            progress.Modify(item);
        }
    }

    private void ModifyActors(List<WorkFlowActorArg> args)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveActors = _workFlowActors.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveActors)
        {
            item.Delete();
        }
        var existsBpmnIds = _workFlowActors.Select(x => x.BpmnId);
        var notExistsActors = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddActors(notExistsActors);
        var existActorArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existActorArgs)
        {
            var actor = _workFlowActors.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            actor.Modify(item);
        }
    }

    private void ModifySteps(List<StepArg> args)
    {
        var allBmpnIds = args.Select(x => x.BpmnId);
        var deActiveSteps = _step.Where(x => !allBmpnIds.Contains(x.BpmnId));
        foreach (var item in deActiveSteps)
        {
            item.Delete();
        }
        var existsBpmnIds = _step.Select(x => x.BpmnId);
        var notExistsSteps = args.Where(x => !existsBpmnIds.Contains(x.BpmnId));
        AddSteps(notExistsSteps);
        var existActorArgs = args.Where(x => existsBpmnIds.Contains(x.BpmnId));
        foreach (var item in existActorArgs)
        {
            var actor = _step.FirstOrDefault(x => x.BpmnId == item.BpmnId);
            actor.Modify(item);
        }
    }

    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;

    }
    public bool DeleteStep(long stepId)
    {
        var result = _step.Where(x => x.Id == new StepId(stepId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;
    }
    public bool DeleteState(long stateId)
    {
        var result = _states.Where(x => x.Id == new StateId(stateId)).FirstOrDefault();
        if (result is not null)
        {
            result.Delete();
            return true;
        }
        else
            return false;
    }
    public async Task ModifyStep(ModifyStepArgs arg)
    {
        var result = _step.Where(x => x.Id == new StepId(arg.Id)).FirstOrDefault();
        //ToDo Sanaz Should return error if it is null
        if (result is not null)
        {
            result.Modify(arg);
        }
    }
    public async Task ModifyState(ModifyStateArgs arg , IWorkFlowDomainService service)
    {
        var result = _states.Where(x => x.Id == new StateId(arg.Id)).FirstOrDefault();
        //ToDo Sanaz should return error if it is null!!!
        if (result is not null)
        {
             await result.Modify(arg , service);
        }
    }
    public Step AddStep(StepArg arg )
    {
        var step = Step.New(arg);
        _step.Add(step);
        return step;

    }
    public async Task<State> AddState(CreateStateArg arg ,IWorkFlowDomainService service)
    {
        var state = await State.New(arg , service);
        _states.Add(state);
        return state;

    }

    public async Task AddProgress(ProgressArg arg)
    {
        var currentProgress = Progress.Entities.Progress.New(arg);
        _progress.Add(currentProgress);

    }


    private static async Task CreateGuards(CreateWorkFlowArg arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (await service.IsCodeUnique(arg.Code, arg.Id)) throw WorkFlowExceptions.WorkFlowCodeIsUniqueException;
        
    }

    private static async Task ModifyGuards(ModifyWorkFlowArg arg, IWorkFlowDomainService service)
    {
        arg.Name.NullCheck();
        if (arg.Code.Length > 20) throw SimaResultException.LengthCodeException;
        if (await service.IsCodeUnique(arg.Code, arg.Id)) throw WorkFlowExceptions.WorkFlowCodeIsUniqueException;

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
    private List<Issue> _Issues = new();
    public virtual ICollection<Issue> Issues => _Issues;
    private List<IssueChangeHistory> _issueChangeHistories = new();
    public ICollection<IssueChangeHistory> IssueChangeHistories => _issueChangeHistories;
}
