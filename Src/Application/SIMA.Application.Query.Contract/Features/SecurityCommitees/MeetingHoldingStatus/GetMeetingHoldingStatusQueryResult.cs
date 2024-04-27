namespace SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus
{
    public class GetMeetingHoldingStatusQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
        public long ActiveStatusId { get; set; }
    }
}
