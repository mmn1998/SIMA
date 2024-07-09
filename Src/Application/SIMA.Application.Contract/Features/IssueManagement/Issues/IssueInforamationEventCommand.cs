namespace SIMA.Application.Contract.Features.IssueManagement.Issues
{
    public class IssueInforamationEventCommand
    {
        public string DueDate { get; set; }
        public long IssuePriorityId { get; set; }
        public int Weight { get; set; }
    }
}
