using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceLevels;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ConsequenceLevels.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/ConsequenceLevels")]
public class ConsequenceLevelsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceLevelsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.ConsequenceLevelGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceLevelQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.ConsequenceLevelGetAll)]
    public async Task<Result> Get([FromBody] GetAllConsequenceLevelsQuery query)
    {
        return await _mediator.Send(query);
    }
}