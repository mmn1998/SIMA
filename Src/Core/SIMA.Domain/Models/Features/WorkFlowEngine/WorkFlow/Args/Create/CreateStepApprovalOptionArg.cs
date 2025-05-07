namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateStepApprovalOptionArg
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public long ActiveStatusId { get; set; }
        public long ApprovalOptionId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
