using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Notifications.Messages;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Notifications.Messages
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Notification/Messages")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.NotificationPost)]
        public async Task<Result> Post([FromBody] CreateMessageCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("notificationMessage")]
        [SimaAuthorize(Permissions.NotificationMessagePost)]
        public async Task<Result> Post([FromBody] CreateMessageSeenStatisticsCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.NotificationPut)]
        public async Task<Result> Put([FromBody] ModifyMessageCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.NotificationDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteMessageCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
