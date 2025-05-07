using AutoMapper;
using Azure.Core;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Notifications.Messages;
using SIMA.Domain.Models.Features.Notifications.Messages.Args;
using SIMA.Domain.Models.Features.Notifications.Messages.Contracts;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Staffs;

namespace SIMA.Application.Feaatures.Notifications.Messages
{
    public class MessageCommandHandler : 
        ICommandHandler<CreateMessageCommand, Result<long>>,
        ICommandHandler<ModifyMessageCommand, Result<long>>,
        ICommandHandler<DeleteMessageCommand, Result<long>>,
        ICommandHandler<CreateMessageSeenStatisticsCommand, Result<long>>
    {
        private readonly IMessageRepository _repository;
        private readonly IMessageDomainService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimaIdentity _simaIdentity;
        private readonly IMapper _mapper;
        private readonly IStaffQueryRepository _staffQueryRepository;

        public MessageCommandHandler(IMessageRepository repository, IMessageDomainService service,
            IUnitOfWork unitOfWork, ISimaIdentity simaIdentity, IMapper mapper , IStaffQueryRepository staffQueryRepository)
        {
            _repository = repository;
            _service = service;
            _unitOfWork = unitOfWork;
            _simaIdentity = simaIdentity;
            _mapper = mapper;
            _staffQueryRepository = staffQueryRepository;
        }
        public async Task<Result<long>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var arg = _mapper.Map<CreateMessageArg>(request);
                arg.CreatedBy = _simaIdentity.UserId;
                var entity = await Message.Create(arg, _service);

                if (request.NotificationMessageGroupDisplayList is not null && request.NotificationMessageGroupDisplayList.Count > 0)
                {
                    var messageGroup = _mapper.Map<List<CreateMessageGroupDisplayArg>>(request.NotificationMessageGroupDisplayList);
                    foreach (var permission in messageGroup)
                    {
                        permission.CreatedBy = _simaIdentity.UserId;
                    }
                    await entity.MessageGroup(messageGroup, entity.Id.Value);
                }

                if ( request.NotificationMessagePositionDisplayList is not null && request.NotificationMessagePositionDisplayList.Count > 0)
                {
                    var messagePosition = _mapper.Map<List<CreateMessagePositionDisplayArg>>(request.NotificationMessagePositionDisplayList);
                    foreach (var permission in messagePosition)
                    {
                        permission.CreatedBy = _simaIdentity.UserId;
                    }
                    await entity.MessagePosition(messagePosition, entity.Id.Value);
                }

                if (request.NotificationMessageAttachmentDisplayList is not null && request.NotificationMessageAttachmentDisplayList.Count > 0)
                {
                    var messageAttachment = _mapper.Map<List<CreateMessageAttachmentArg>>(request.NotificationMessageAttachmentDisplayList);
                    foreach (var permission in messageAttachment)
                    {
                        permission.CreatedBy = _simaIdentity.UserId;
                    }
                    await entity.MessageAttachment(messageAttachment, entity.Id.Value);
                }

                await _repository.Add(entity);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(arg.Id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<Result<long>> Handle(ModifyMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyMessageArg>(request);
            arg.ModifiedBy = _simaIdentity.UserId;
            await entity.Modify(arg, _service);

            if (request.NotificationMessageGroupDisplayList is not  null && request.NotificationMessageGroupDisplayList.Count > 0  )
            {
                var messageGroup = _mapper.Map<List<CreateMessageGroupDisplayArg>>(request.NotificationMessageGroupDisplayList);
                foreach (var permission in messageGroup)
                {
                    permission.CreatedBy = _simaIdentity.UserId;
                }
                await entity.MessageGroup(messageGroup, entity.Id.Value);
            }

            if (request.NotificationMessagePositionDisplayList is not null && request.NotificationMessagePositionDisplayList.Count > 0 )
            {
                var messagePosition = _mapper.Map<List<CreateMessagePositionDisplayArg>>(request.NotificationMessagePositionDisplayList);
                foreach (var permission in messagePosition)
                {
                    permission.CreatedBy = _simaIdentity.UserId;
                }
                await entity.MessagePosition(messagePosition, entity.Id.Value);
            }

            if (request.NotificationMessageAttachmentDisplayList is not null && request.NotificationMessageAttachmentDisplayList.Count > 0  )
            {
                var messageAttachment = _mapper.Map<List<CreateMessageAttachmentArg>>(request.NotificationMessageAttachmentDisplayList);
                foreach (var permission in messageAttachment)
                {
                    permission.CreatedBy = _simaIdentity.UserId;
                }
                await entity.MessageAttachment(messageAttachment, entity.Id.Value);
            }

            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            long userId = _simaIdentity.UserId; entity.Delete(userId);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(request.Id);
        }

        public async Task<Result<long>> Handle(CreateMessageSeenStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _repository.GetById(request.MessageId);
                var staffArg = _mapper.Map<CreateMessageSeenStatisticsArg>(request);
                staffArg.StaffId = await _staffQueryRepository.GetStaffIdByUserId(_simaIdentity.UserId);
                staffArg.CreatedBy = _simaIdentity.UserId;
                await entity.AddMessageSeenStatistics(staffArg);
                await _unitOfWork.SaveChangesAsync();
                return Result.Ok(entity.Id.Value);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }
    }
}
