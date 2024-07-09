namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify
{
    public class ModifyStepArgs
    {

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? CompleteName { get; set; }
        public long? WorkFlowId { get; set; }
        public long FormId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public string HasDocument { get; set; }

    }
}
