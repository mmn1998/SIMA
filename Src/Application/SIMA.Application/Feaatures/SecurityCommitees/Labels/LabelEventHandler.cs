using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;

namespace SIMA.Application.Feaatures.SecurityCommitees.Labels
{
    public class LabelEventHandler : INotificationHandler<MeetingCreatedEvent>
    {
        private readonly ILabelRepository _repository;
        private readonly IMapper _mapper;

        public LabelEventHandler(ILabelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.labels)
            {
                var entity = await Label.Create(item);
                await _repository.Add(entity);
            }
        }
    }
}
