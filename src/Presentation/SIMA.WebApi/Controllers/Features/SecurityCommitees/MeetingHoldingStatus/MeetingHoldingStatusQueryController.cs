using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.MeetingHoldingStatus
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MeetingHoldingStatus")]
    public class MeetingHoldingStatusQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MeetingHoldingStatusQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<Result> Get([FromQuery] GetAllMeetingHoldingStatusQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetMeetingHoldingStatusQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
