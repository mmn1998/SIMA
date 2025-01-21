using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class MessageChangeHistory : Entity
    {
        private MessageChangeHistory()
        {
            
        }
        private MessageChangeHistory(CreateMessageChangeHistoryArg arg)
        {
            Id = new(arg.Id);
            Subject = arg.Subject;
            Description = arg.Description;
            ExpireDate = arg.ExpireDate;
            ActiveStatusId = arg.ActiveStatusId;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public async static Task<MessageChangeHistory> Create(CreateMessageChangeHistoryArg arg, IGoodsTypeDomainService service)
        {
            return new MessageChangeHistory(arg);
        }
       

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public MessageChangeHistoryId Id { get; private set; }
        public MessageId MessageId { get; private set; }
        public virtual Message Message { get; private set; }
        public string Subject { get; private set; }
        public string Description { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

    }
}
