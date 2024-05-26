using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.Cartables;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.MeetingHoldingReasons;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.MeetingHoldingReasons.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "MeetingHoldingReasons")]
public class MeetingHoldingReasonsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingHoldingReasonsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetMeetingHoldingReasonQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    public async Task<Result> Get([FromQuery] GetAllMeetingHoldingReasonsQuery query)
    {
        return await _mediator.Send(query);
    }
}
