using SIMA.Domain.Models.Features.Auths.Groups.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Positions.ValueObjects;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.ValueObjects;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.Notifications.Messages.Entities
{
    public class Message :Entity
    {
        private Message()
        {
            
        }
        private Message(CreateMessageArg arg)
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
        public async static Task<Message> Create(CreateMessageArg arg, IMessageDomainService service)
        {
            return new Message(arg);
        }
        public async Task Modify(ModifyMessageArg arg, IMessageDomainService service)
        {
            Subject = arg.Subject;
            Description = arg.Description;
            ExpireDate = arg.ExpireDate;
            ActiveStatusId = arg.ActiveStatusId;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }

        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }


        #region OtherMethod

        public async Task MessageGroup(List<CreateMessageGroupDisplayArg> request, long messageId)
        {
            messageId.NullCheck();

            var previousEntity = _messageGroupDisplay.Where(x => x.MessageId == new MessageId(messageId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

            var addEntity = request.Where(x => !previousEntity.Any(c => c.GroupId.Value == x.GroupId)).ToList();
            var deleteEntity = previousEntity.Where(x => !request.Any(c => c.GroupId == x.GroupId.Value)).ToList();


            foreach (var item in addEntity)
            {
                var entity = _messageGroupDisplay.Where(x => (x.GroupId == new GroupId(item.GroupId) && x.MessageId == new MessageId(messageId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
                if (entity is not null)
                {
                    entity.ChangeStatus(ActiveStatusEnum.Active);
                }
                else
                {
                    entity = await Entities.MessageGroupDisplay.Create(item);
                    _messageGroupDisplay.Add(entity);
                }
            }

            foreach (var item in deleteEntity)
            {
                item.Delete((long)request[0].CreatedBy);
            }
        }

        public async Task MessagePosition(List<CreateMessagePositionDisplayArg> request, long messageId)
        {
            messageId.NullCheck();

            var previousEntity = _messagePositionDisplay.Where(x => x.MessageId == new MessageId(messageId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

            var addEntity = request.Where(x => !previousEntity.Any(c => c.PositionId.Value == x.PositionId)).ToList();
            var deleteEntity = previousEntity.Where(x => !request.Any(c => c.PositionId == x.PositionId.Value)).ToList();


            foreach (var item in addEntity)
            {
                var entity = _messagePositionDisplay.Where(x => (x.PositionId == new PositionId(item.PositionId) && x.MessageId == new MessageId(messageId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
                if (entity is not null)
                {
                    entity.ChangeStatus(ActiveStatusEnum.Active);
                }
                else
                {
                    entity = await Entities.MessagePositionDisplay.Create(item);
                    _messagePositionDisplay.Add(entity);
                }
            }

            foreach (var item in deleteEntity)
            {
                item.Delete((long)request[0].CreatedBy);
            }
        }

        public async Task MessageAttachment(List<CreateMessageAttachmentArg> request, long messageId)
        {
            messageId.NullCheck();

            var previousEntity = _messageAttachments.Where(x => x.MessageId == new MessageId(messageId) && x.ActiveStatusId == (long)ActiveStatusEnum.Active);

            var addEntity = request.Where(x => !previousEntity.Any(c => c.DocumentId.Value == x.DocumentId)).ToList();
            var deleteEntity = previousEntity.Where(x => !request.Any(c => c.DocumentId == x.DocumentId.Value)).ToList();


            foreach (var item in addEntity)
            {
                var entity = _messageAttachments.Where(x => (x.DocumentId == new DocumentId(item.DocumentId) && x.MessageId == new MessageId(messageId)) && x.ActiveStatusId != (long)ActiveStatusEnum.Active).FirstOrDefault();
                if (entity is not null)
                {
                    entity.ChangeStatus(ActiveStatusEnum.Active);
                }
                else
                {
                    entity = await Entities.MessageAttachment.Create(item);
                    _messageAttachments.Add(entity);
                }
            }

            foreach (var item in deleteEntity)
            {
                item.Delete((long)request[0].CreatedBy);
            }
        }


        public async Task AddMessageSeenStatistics(CreateMessageSeenStatisticsArg request)
        {
            var messageSeen = _messageSeenStatisticses.FirstOrDefault(x=>x.MessageId.Value == request.MessageId);
            if (messageSeen is null)
            {
                var entity = await MessageSeenStatistics.Create(request);
                _messageSeenStatisticses.Add(entity);
            }
        }


        #endregion

        public MessageId Id { get; private set; }
        public string Subject { get; private set; }  
        public string Description { get; private set; }  
        public DateTime? ExpireDate { get; private set; }  
        public long ActiveStatusId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public long CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }

        private List<MessagePositionDisplay> _messagePositionDisplay = new();
        public ICollection<MessagePositionDisplay> MessagePositionDisplay => _messagePositionDisplay;

        private List<MessageChangeHistory> _messageChangeHistories = new();
        public ICollection<MessageChangeHistory> MessageChangeHistories => _messageChangeHistories;

        private List<MessageAttachment> _messageAttachments = new();
        public ICollection<MessageAttachment> MessageAttachments => _messageAttachments;

        private List<MessageGroupDisplay> _messageGroupDisplay = new();
        public ICollection<MessageGroupDisplay> MessageGroupDisplay => _messageGroupDisplay;

        private List<MessageSeenStatistics> _messageSeenStatisticses = new();
        public ICollection<MessageSeenStatistics> MessageSeenStatisticses => _messageSeenStatisticses;

    }
}
