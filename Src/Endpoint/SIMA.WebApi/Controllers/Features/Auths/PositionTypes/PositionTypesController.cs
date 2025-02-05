using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.PositionTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.PositionTypes;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PositionTypes")]
[Authorize]
public class PositionTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreatePositionTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyPositionTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePositionTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}