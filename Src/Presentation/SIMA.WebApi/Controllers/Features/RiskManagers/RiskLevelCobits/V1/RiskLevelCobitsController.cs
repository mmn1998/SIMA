using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevelCobits;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevelCobits.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/RiskLevelCobits")]
public class RiskLevelCobitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RiskLevelCobitsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.RiskLevelCobitPost)]
    public async Task<Result> Post([FromBody] CreateRiskLevelCobitCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.RiskLevelCobitPut)]
    public async Task<Result> Put([FromBody] ModifyRiskLevelCobitCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.RiskLevelCobitDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRiskLevelCobitCommand { Id = id };
        return await _mediator.Send(command);
    }
}