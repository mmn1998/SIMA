using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;


namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class MessageGroupDisplay : Entity
    {
        private MessageGroupDisplay()
        {
            
        }
        private MessageGroupDisplay(CreateMessageGroupDisplayArg arg)
        {
            Id = new(arg.Id);
            MessageId = new(arg.MessageId);
            GroupId = new(arg.GroupId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public async static Task<MessageGroupDisplay> Create(CreateMessageGroupDisplayArg arg)
        {
            return new MessageGroupDisplay(arg);
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

        public MessageGroupDisplayId Id { get; private set; }
        public MessageId MessageId { get; private set; }
        public virtual Message Message { get; private set; }
        public GroupId GroupId { get; private set; }
        public virtual Group Group { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}
