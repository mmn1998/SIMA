using AutoMapper;
using MediatR;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.SecurityCommitees.Subject;

public class SubjectEventHandler : INotificationHandler<MeetingCreatedEvent>
{

    private readonly ISubjectRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISimaIdentity _simaIdentity;

    public SubjectEventHandler(ISubjectRepository repository, IMapper mapper, ISimaIdentity simaIdentity)
    {
        _repository = repository;
        _mapper = mapper;
        _simaIdentity = simaIdentity;
    }
    public async Task Handle(MeetingCreatedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var item in notification.SubjectArgs)
        {
            item.CreatedBy = _simaIdentity.UserId;
            var entity = await Domain.Models.Features.SecurityCommitees.Subjects.Entities.Subject.Create(item);
            await _repository.Add(entity);
        }
    }
}
