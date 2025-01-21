namespace SIMA.Domain.Models.Features.Notifications.Messages.Args
{
    public class CreateMessagePositionDisplayArg 
    {
        public long Id { get; set; }
        public long MessageId { get; set; }
        public long PositionId { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
}
