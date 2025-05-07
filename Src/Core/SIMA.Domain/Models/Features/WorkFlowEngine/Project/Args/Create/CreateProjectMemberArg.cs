using SIMA.Domain.Models.Features.WorkFlowEngine.Project.ValueObjects;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create
{
    public class CreateProjectMemberArg
    {
        public long Id { get; set; }
        public ProjectId ProjectId { get; set; }
        public long UserId { get; set; }
        public string IsManager { get; set; } 
        public string IsAdminProject { get; set; } 
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? ActiveStatusId { get; set; }

    }
}
