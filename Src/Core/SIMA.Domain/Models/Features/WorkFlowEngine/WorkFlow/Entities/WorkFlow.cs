using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Interface;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
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
        ManagerRoleId = arg.ManagerRoleId;
        ActiveStatusId = arg.ActiveStatusId;
        MainAggregateId = arg.MainAggregateId;
        Ordering = arg.Ordering;

    }
    public static WorkFlow New(CreateWorkFlowArg arg)
    {
        return new WorkFlow(arg);
    }
    public void AddSteps(List<CreateStepArg> args)
    {
        foreach (var item in args)
        {
            var step = Step.New(item);
            step.AddActorStep(item.ActorStepArgs);
            _step.Add(step);
        }
    }
    public void AddProgresses(List<CreateProgressArg> args)
    {
        var progresses = args.Select(Progress.Entities.Progress.New);

        _progress.AddRange(progresses);
    }
    public void AddActors(List<CreateWorkFlowActorArg> args)
    {
        var actors = args.Select(WorkFlowActor.Entites.WorkFlowActor.New);
        _workFlowActors.AddRange(actors);
    }
    public void Modify(ModifyWorkFlowArg arg)
    {
        Code = arg.Code;
        Name = arg.Name;
        BpmnId = arg.BpmnId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        MainAggregateId = arg.MainAggregateId;
        Ordering = arg.Ordering;
    }

    public void Modify(ModifyFileContentArg arg)
    {
        BpmnId = arg.BpmnId;
        //ModifiedAt = arg.ModifyAt;
        ModifiedBy = arg.ModifyBy;
        FileContent = arg.FileContent;
        AddSteps(arg.Steps);
        AddActors(arg.WorkFlowActors);
        AddProgresses(arg.Progresses);
    }
    public void Deactive()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Deactive;

    }
    public bool DeactiveStep(long stepId)
    {
        var result = _step.Where(x => x.Id == new StepId(stepId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;
    }
    public bool DeactiveState(long stateId)
    {
        var result = _states.Where(x => x.Id == new StateId(stateId)).FirstOrDefault();
        if (result is not null)
        {
            result.Deactive();
            return true;
        }
        else
            return false;
    }
    public void ModifyStep(ModifyStepArgs arg)
    {
        var result = _step.Where(x => x.Id == new StepId(arg.Id)).FirstOrDefault();
        //ToDo Sanaz Should return error if it is null
        if (result is not null)
        {
            result.Modify(arg);
        }
    }
    public async void ModifyState(ModifyStateArgs arg , IWorkFlowDomainService service)
    {
        var result = _states.Where(x => x.Id == new StateId(arg.Id)).FirstOrDefault();
        //ToDo Sanaz should return error if it is null!!!
        if (result is not null)
        {
             result.Modify(arg , service);
        }
    }
    public Step AddStep(CreateStepArg arg )
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

    public async Task AddProgress(CreateProgressArg arg)
    {
        var currentProgress = Progress.Entities.Progress.New(arg);
        _progress.Add(currentProgress);

    }

    //protected override string ValidateInvariants()
    //{
    //    throw new NotImplementedException();
    //}

    public WorkFlowId Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public ProjectId ProjectId { get; set; }
    public string? FileContent { get; set; }
    public long? MainAggregateId { get; set; }
    public long? ManagerRoleId { get; set; }
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
}
