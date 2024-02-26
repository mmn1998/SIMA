namespace SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify
{
    public class ModifyProjectGroupArg
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long GroupId { get; set; }
        public long? ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
