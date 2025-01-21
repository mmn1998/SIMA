using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Notifications.Messages
{
    public class ModifyMessageCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? ExpireDate { get; set; }
        public List<CreateNotificationMessagePositionDisplay>? NotificationMessagePositionDisplayList { get; set; }
        public List<CreateNotificationMessageGroupDisplay>? NotificationMessageGroupDisplayList { get; set; }
        public List<CreateNotificationMessageAttachmentDisplay>? NotificationMessageAttachmentDisplayList { get; set; }
    }
}
