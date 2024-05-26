using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Positions.V1;
[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Positions")]
[Authorize]

public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [SimaAuthorize(Permissions.PositionsDelete)]

    [HttpPost]
    [SimaAuthorize(Permissions.PositionsPost)]
    public async Task<Result> Post([FromBody] CreatePositionCommand request)
    {
        var response = await _mediator.Send(request);
        return response;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.PositionsPut)]
    public async Task<Result> Put([FromBody] ModifyPositionCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.PositionsDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeletePositionCommand { Id = id };
        return await _mediator.Send(command);
    }
}
