namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify
{
    public class ModifyRejectionReasonArg
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
