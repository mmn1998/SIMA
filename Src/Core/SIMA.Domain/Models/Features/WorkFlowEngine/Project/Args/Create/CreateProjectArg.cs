namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create
{
    public class CreateProjectArg 
    {
        public long? DomainId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
