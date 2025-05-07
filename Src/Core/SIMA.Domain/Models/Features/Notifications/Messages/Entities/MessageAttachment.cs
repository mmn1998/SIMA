using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class MessageAttachment : Entity
    {
        private MessageAttachment()
        {
            
        }
        private MessageAttachment(CreateMessageAttachmentArg arg)
        {
            Id = new(arg.Id);
            MessageId = new(arg.MessageId);
            DocumentId = new(arg.DocumentId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public async static Task<MessageAttachment> Create(CreateMessageAttachmentArg arg)
        {
            return new MessageAttachment(arg);
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public void ChangeStatus(ActiveStatusEnum status)
        {
            ActiveStatusId = (long)status;
        }

        public MessageAttachmentId Id { get; private set; }
        public MessageId MessageId { get; private set; }
        public virtual Message Message { get; private set; }
        public DocumentId DocumentId { get; private set; }
        public virtual Document Document { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
