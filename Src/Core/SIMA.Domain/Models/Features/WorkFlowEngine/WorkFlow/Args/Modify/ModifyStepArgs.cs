namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify
{
    public class ModifyStepArgs
    {

        public long Id { get; set; }
        public string? Name { get; set; }
        public long? WorkFlowId { get; set; }
        //public long? ActionTypeId { get; set; }
        ///public long FormId { get; set; }

        //public long? MainEntityId { get; set; }
        public long? StateId { get; set; }
        //public string? BpmnId { get; private set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
