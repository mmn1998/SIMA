namespace SIMA.Domain.Models.Features.Notifications.Messages.Args
{
    public class ModifyMessageArg
    {
        public long Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpireDate { get; set; }
        public long ActiveStatusId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
