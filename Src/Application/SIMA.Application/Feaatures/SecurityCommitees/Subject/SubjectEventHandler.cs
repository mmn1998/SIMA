using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;

namespace SIMA.Application.Feaatures.SecurityCommitees.Subject
{
    public class SubjectEventHandler : INotificationHandler<MeetingCreatedEvent>
    {

        private readonly ISubjectRepository _repository;
        private readonly IMapper _mapper;

        public SubjectEventHandler(ISubjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.SubjectArgs)
            {
                var entity = await Domain.Models.Features.SecurityCommitees.Subjects.Entities.Subject.Create(item);
                await _repository.Add(entity);
            }
        }
    }
}
