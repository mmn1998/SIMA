namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args
{
    public class CreateServiceTaskArg
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public long APIMethodActionId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }

    }
}
