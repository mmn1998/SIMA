namespace SIMA.Domain.Models.Features.WorkFlowEngine.ServiceTasks.Args
{
    public class CreateInputParamArg
    {
        public long? Id { get; set; }
        public string? InputName { get; set; }
        public long DataTypeId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
