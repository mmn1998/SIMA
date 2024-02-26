namespace SIMA.Application.Contract.Features.WorkFlowEngine.Project
{
    public class ProjectMemberListCommand
    {
        public string IsManager { get; set; }
        public string IsAdminProject { get; set; }
        public long UserId { get; set; }
    }
}
