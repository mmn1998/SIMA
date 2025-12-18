using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.CobitScenarios;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CobitScenarios.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/CobitScenarios")]
public class CobitScenariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public CobitScenariosController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.CobitScenarioPost)]
    public async Task<Result> Post([FromBody] CreateCobitScenarioCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.CobitScenarioPut)]
    public async Task<Result> Put([FromBody] ModifyCobitScenarioCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.CobitScenarioDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCobitScenarioCommand { Id = id };
        return await _mediator.Send(command);
    }
}