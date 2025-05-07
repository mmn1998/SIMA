using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class MessagePositionDisplay : Entity
    {
        private MessagePositionDisplay()
        {
            
        }
        private MessagePositionDisplay(CreateMessagePositionDisplayArg arg)
        {
            Id = new(arg.Id);
            MessageId = new(arg.MessageId);
            PositionId = new(arg.PositionId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public async static Task<MessagePositionDisplay> Create(CreateMessagePositionDisplayArg arg)
        {
            return new MessagePositionDisplay(arg);
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

        public MessagePositionDisplayId Id { get; private set; }
        public MessageId MessageId { get; private set; }
        public virtual Message Message { get; private set; }
        public PositionId PositionId { get; private set; }
        public virtual Position Position { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
