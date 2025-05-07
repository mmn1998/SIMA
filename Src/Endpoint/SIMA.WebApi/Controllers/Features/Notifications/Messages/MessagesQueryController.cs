using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Notifications.Messages;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Notifications.Messages
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Notification/Messages")]
    public class MessagesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.NotificationGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetMessageQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.NotificationGetAll)]
        public async Task<Result> Get([FromBody] GetAllMessagesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("GetAllForUser")]
        [SimaAuthorize(Permissions.NotificationGetAllForUser)]
        public async Task<Result> GetAllForUser([FromBody] GetAllMessageForUserQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("GetSeenCountNotification")]
        [SimaAuthorize(Permissions.NotificationGetAllForUser)]
        public async Task<Result> GetSeenCountNotification()
        {
            var query = new GetCountSeenNotificationQuery();
            return await _mediator.Send(query);
        }
    }
}
