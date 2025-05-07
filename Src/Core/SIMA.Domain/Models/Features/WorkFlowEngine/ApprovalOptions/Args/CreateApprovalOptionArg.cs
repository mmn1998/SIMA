namespace SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Args
{
    public class CreateApprovalOptionArg
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }

    }
}
