namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args
{
    public class ModifyServiceTaskArg
    {
        public string Address { get; set; }
        public long APIMethodActionId { get; set; }
        public long ActiveStatusId { get; set; }
        public long? ModifiedBy { get; set; }
        public byte[]? ModifiedAt { get; set; }
    }
}
