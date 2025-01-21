namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetNotificationMessageGroupDisplay
    {
        public long GroupId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long BranchId { get; set; }
        public string? BranchName { get; set; }
        public long ActiveStatusId { get; set; }
        public string? ActiveStatusName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
