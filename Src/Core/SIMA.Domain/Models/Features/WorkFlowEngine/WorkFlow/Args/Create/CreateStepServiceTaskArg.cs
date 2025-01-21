namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateStepServiceTaskArg
    {
        public long Id { get; set; }
        public long ServiceTaskId { get; set; }
        public long StepId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
