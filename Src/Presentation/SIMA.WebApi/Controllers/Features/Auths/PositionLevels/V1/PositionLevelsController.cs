using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.PositionLevels;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.PositionLevels.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PositionLevels")]
public class PositionLevelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionLevelsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreatePositionLevelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyPositionLevelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePositionLevelCommand { Id = id };
        return await _mediator.Send(command);
    }
}