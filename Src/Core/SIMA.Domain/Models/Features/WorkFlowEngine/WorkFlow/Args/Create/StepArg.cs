using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class StepArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        public long? ActionTypeId { get; set; }
        public long? MainEntityId { get; set; }
        public long? StateId { get; set; }
        public string? BpmnId { get;  set; }
        public long FormId { get; set; }

        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? UserId { get; set; }

        public List<ProgressArg> CreateProgresses { get; set; } = new();
        public List<CreateWorkFlowActorStepArg> ActorStepArgs { get; set; } = new();
    }
}
