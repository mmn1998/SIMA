namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetNotificationMessageAttachmentDisplay
    {
        public long DocumentId { get; set; }
        public string? Title { get; set; }
        public long DocumentTypeId { get; set; }
        public string? DocumentTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
