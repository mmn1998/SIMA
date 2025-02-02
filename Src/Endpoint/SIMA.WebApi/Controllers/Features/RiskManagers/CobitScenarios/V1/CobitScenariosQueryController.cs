using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.CobitScenarios;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CobitScenarios.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CobitScenarios")]
public class CobitScenariosQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CobitScenariosQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.CobitScenarioGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCobitScenarioQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.CobitScenarioGetAll)]
    public async Task<Result> Get([FromBody] GetAllCobitScenariosQuery query)
    {
        return await _mediator.Send(query);
    }
}