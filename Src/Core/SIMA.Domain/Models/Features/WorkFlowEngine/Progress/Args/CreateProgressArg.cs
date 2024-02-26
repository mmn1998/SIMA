using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args
{
    public class CreateProgressArg
    {
        public long Id { get; set; }
        public long SourceId { get; set; }
        public long? TargetId { get; set; }
        public long WorkFlowId { get; set; }
        public string? Name { get; set; }
        public string? BpmnId { get; set; }
        public string? Description { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public CreateStepArg Source { get; set; }
        public CreateStepArg Target { get; set; }
    }
}
