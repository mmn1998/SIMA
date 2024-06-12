namespace SIMA.Application.Query.Contract.Features.IssueManagement.Issues
{
    public class GetApprovalOptionQueryResult
    {
        public long TargetId { get; set; }
        public long SourceId { get; set; }
        public long WorkFlowId { get; set; }
        public string ProgressName { get; set; }
        public string StepName{ get; set; }
        public long StepApprovalOptionId { get; set; }
        public string ApprovalOptionName { get; set; }
        public long ApprovalOptionId { get; set; }
        public long ApprovalOptionCode { get; set; }

    }
}
