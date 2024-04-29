namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project
{
    public class GetProjectMemberResult
    {
        public long ProjectMemberId { get; set; }
        public long UserId { get; set; }
        public string IsAdminProject { get; set; }
        public string IsManager { get; set; }
        public string Username { get; set; }
        public string? FullName { get; set; }

    }
}
