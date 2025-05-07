namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify
{
    public class ModifyProjectMemberArg
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public string IsManager { get; set; }
        public string IsAdminProject { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
