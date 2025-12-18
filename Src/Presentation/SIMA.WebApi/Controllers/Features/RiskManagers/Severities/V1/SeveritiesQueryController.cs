using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.Severities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Severities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/Severities")]
public class SeveritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeveritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.SeverityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSeverityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.SeverityGetAll)]
    public async Task<Result> Get([FromBody] GetAllSeveritiesQuery query)
    {
        return await _mediator.Send(query);
    }
}