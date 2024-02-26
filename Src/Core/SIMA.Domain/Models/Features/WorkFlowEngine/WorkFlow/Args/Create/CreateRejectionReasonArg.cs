namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateRejectionReasonArg
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
