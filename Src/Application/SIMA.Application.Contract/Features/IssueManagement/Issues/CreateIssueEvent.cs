namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class CreateIssueEvent
    {
        public long MainAggregateId { get; set; }
        public long SourceId { get; set; }
        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public string Code { get; set; }
        public string Summery { get; set; }
        public long CurrentStepId { get; set; }
        public long CurrentStateId { get; set; }
    }
}
