using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingStatus;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.MeetingHoldingStatus
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "MeetingHoldingStatus")]
    public class MeetingHoldingStatusController : Controller
    {
        private readonly IMediator _mediator;

        public MeetingHoldingStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateMeetingHoldingStatusCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyMeetingHoldingStatusCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteMeetingHoldingStatusCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
