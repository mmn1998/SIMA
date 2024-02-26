using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create
{
    public class CreateProjectGroupArg
    {
        public long Id { get; set; }
        public ProjectId ProjectId { get; set; }
        public long GroupId { get; set; }
        public long? ActiveStatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
    }
}
