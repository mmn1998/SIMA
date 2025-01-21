using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.StrategyTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.StrategyTypes.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/StrategyTypes")]
public class StrategyTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StrategyTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.strategyTypePost)]
    public async Task<Result> Post([FromBody] CreateStrategyTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.strategyTypePut)]
    public async Task<Result> Put([FromBody] ModifyStrategyTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.strategyTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteStrategyTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}