namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args
{
    public class CreateServiceInputParamArg
    {
        public long Id { get; set; }
        public long ServiceTaskId { get; set; }
        public long InputParamId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
