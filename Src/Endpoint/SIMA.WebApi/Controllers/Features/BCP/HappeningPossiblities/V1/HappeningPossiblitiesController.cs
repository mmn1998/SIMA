using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.HappeningPossiblities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.HappeningPossiblities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "HappeningPossiblities")]
public class HappeningPossiblitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public HappeningPossiblitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateHappeningPossibilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyHappeningPossibilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteHappeningPossibilityCommand { Id = id };
        return await _mediator.Send(command);
    }
}