namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow
{
    public class WorkflowInfoQueryResult
    {
        public long Id { get; set; }
        public long ActiveStatusId { get; set; }
        public long ProjectId { get; set; }
        public long MainAggregateId { get; set; }
        public IList<StateInfo> States { get; set; }
        public IList<StepInfo> Steps { get; set; }
        public IList<WorkFlowActorInfo> WorkFlowActors { get; set; }
    }

    public class StateInfo
    {
        public long Id { get; set; }
        public long WorkFlowId { get; set; }
    }

    public class StepInfo
    {
        public long Id { get; set; }
        public long ActionTypeId { get; set; }
        public IList<SourceProgressInfo> SourceProgresses { get; set; }
    }

    public class SourceProgressInfo
    {
        public long Id { get; set; }
        public long SourceId { get; set; }
        public long StateId { get; set; }
        public long TargetId { get; set; }
    }

    public class WorkFlowActorInfo
    {
        public long Id { get; set; }
        public long WorkFlowId { get; set; }
        public string? IsEveryOne { get; set; }
        public IList<WorkFlowActorStepInfo> WorkFlowActorSteps { get; set; }
        public IList<WorkFlowActorUserInfo> WorkFlowActorUsers { get; set; }
    }

    public class WorkFlowActorStepInfo
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public long WorkFlowActorId { get; set; }
    }

    public class WorkFlowActorUserInfo
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ActiveStatusId { get; set; }
        public long WorkFlowActorId { get; set; }
    }
}
