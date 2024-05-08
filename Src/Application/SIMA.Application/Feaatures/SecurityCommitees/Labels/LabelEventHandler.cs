using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.SecurityCommitees.Labels
{
    public class LabelEventHandler : INotificationHandler<MeetingCreatedEvent>
    {
        private readonly ILabelRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISimaIdentity _simaIdentity;

        public LabelEventHandler(ILabelRepository repository, IMapper mapper, ISimaIdentity simaIdentity)
        {
            _repository = repository;
            _mapper = mapper;
            _simaIdentity = simaIdentity;
        }
        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.labels)
            {
                item.CreatedBy = _simaIdentity.UserId;
                var entity = await Label.Create(item);
                await _repository.Add(entity);
            }
        }
    }
}
