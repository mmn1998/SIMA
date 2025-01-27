using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.Severities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Severities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/Severities")]
public class SeveritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeveritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.SeverityPost)]
    public async Task<Result> Post([FromBody] CreateSeverityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.SeverityPut)]
    public async Task<Result> Put([FromBody] ModifySeverityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.SeverityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSeverityCommand { Id = id };
        return await _mediator.Send(command);
    }
}