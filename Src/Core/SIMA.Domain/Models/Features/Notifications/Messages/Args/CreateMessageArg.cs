using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Args
{
    public class CreateMessageArg
    {
        public long Id { get;  set; }
        public string? Subject { get;  set; }
        public string? Description { get;  set; }
        public DateTime? ExpireDate { get;  set; }
        public long ActiveStatusId { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public long CreatedBy { get;  set; }

    }
}
