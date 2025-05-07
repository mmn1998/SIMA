using Microsoft.AspNetCore.Http.HttpResults;
using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetMessageQueryResult
    {
        public long Id { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? HasDocument { get; set; }
        public string? IsSeen { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ExpireDatePersian => DateHelper.ToPersianDateTime(ExpireDate);
        public List<GetNotificationMessageAttachmentDisplay>? NotificationMessageAttachmentDisplayList { get; set; }
        public List<GetNotificationMessagePositionDisplay>?   NotificationMessagePositionDisplayList { get; set; }
        public List<GetNotificationMessageGroupDisplay>? NotificationMessageGroupDisplayList { get; set; }
    }
}
