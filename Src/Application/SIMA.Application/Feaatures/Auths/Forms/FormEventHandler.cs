using MediatR;
using SIMA.Domain.Models.Features.Auths.Groups.Event;

namespace SIMA.Application.Feaatures.Auths.Forms
{
    internal class FormEventHandler : INotificationHandler<FromGroupPermissionEvent>
    {
        public Task Handle(FromGroupPermissionEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
