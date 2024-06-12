namespace SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Args
{
    public class ModifyApprovalOptionArg
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ActiveStatusId { get; set; }
        public long? ModifiedBy { get; set; }
        public byte[]? ModifiedAt { get; set; }
    }
}
