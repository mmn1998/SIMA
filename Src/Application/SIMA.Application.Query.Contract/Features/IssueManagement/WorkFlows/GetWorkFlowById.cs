namespace SIMA.Application.Query.Contract.Features.IssueManagement.WorkFlows
{
    public class GetWorkFlowById
    {
        public long Id { get; set; }
        public long CurrentStepId { get; set; }
        public long CurrentStateId { get; set; }
    }
}
