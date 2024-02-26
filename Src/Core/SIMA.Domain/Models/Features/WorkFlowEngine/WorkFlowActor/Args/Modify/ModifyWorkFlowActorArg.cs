namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify
{
    public class ModifyWorkFlowActorArg
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long WorkFlowId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
