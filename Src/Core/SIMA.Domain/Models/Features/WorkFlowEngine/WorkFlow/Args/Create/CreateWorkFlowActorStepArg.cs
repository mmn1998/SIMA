namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateWorkFlowActorStepArg
    {
        public long WorkFlowActorId { get; set; }
        public long StepId { get; set; }
        public long ActiveStatusId { get; set; }
        public string BpmnId { get; set; }
        public string ActorBpmnId { get; set; }
        public long Id { get; set; }
    }
}
