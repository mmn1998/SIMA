using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class MessageSeenStatistics : Entity
    {
        private MessageSeenStatistics()
        {
            
        }
        private MessageSeenStatistics(CreateMessageSeenStatisticsArg arg)
        {
            Id = new(IdHelper.GenerateUniqueId());
            MessageId = new(arg.MessageId);
            StaffId = new(arg.StaffId);
            ActiveStatusId = arg.ActiveStatusId;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }
        public async static Task<MessageSeenStatistics> Create(CreateMessageSeenStatisticsArg arg)
        {
            return new MessageSeenStatistics(arg);
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }

        public MessageSeenStatisticsId Id { get; private set; }
        public MessageId MessageId { get; private set; }
        public virtual Message Message { get; private set; }
        public StaffId StaffId { get; private set; }
        public virtual Staff Staff { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
