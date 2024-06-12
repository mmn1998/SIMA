namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify
{
    public class ModifyStepApprovalOptionArg
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public long ApprovalOptionId { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
