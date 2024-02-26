namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args
{
    public class ModifyProgressArg
    {
        public long SourceId { get; set; }
        public long? TargetId { get; set; }
        public long WorkFlowId { get; set; }
        public string? Name { get; set; }
        public string? BpmnId { get; set; }
        public string? Description { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get;  set; }
        public long? ModifiedBy { get;  set; }
    }
}
