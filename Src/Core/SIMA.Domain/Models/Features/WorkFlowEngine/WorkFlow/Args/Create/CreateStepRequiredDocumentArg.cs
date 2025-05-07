namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create
{
    public class CreateStepRequiredDocumentArg
    {
        public long Id { get; set; }
        public long StepId { get; set; }
        public long DocumentTypeId { get; set; }
        public int Count { get; set; }
        public long? ActiveStatusId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
