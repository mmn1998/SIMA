using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.MeetingHoldingReasons.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "MeetingHoldingReasons")]
public class MeetingHoldingReasonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingHoldingReasonsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateMeetingHoldingReasonCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyMeetingHoldingReasonCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteMeetingHoldingReasonCommand { Id = id };
        return await _mediator.Send(command);
    }
}
