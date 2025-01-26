using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.ConsequenceLevels;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ConsequenceLevels.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/ConsequenceLevels")]
public class ConsequenceLevelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceLevelsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.ConsequenceLevelPost)]
    public async Task<Result> Post([FromBody] CreateConsequenceLevelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.ConsequenceLevelPut)]
    public async Task<Result> Put([FromBody] ModifyConsequenceLevelCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.ConsequenceLevelDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceLevelCommand { Id = id };
        return await _mediator.Send(command);
    }
}