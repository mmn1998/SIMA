using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.LogisticsSupplies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.LogisticsSupplies.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LogisticsSupplies")]
[Authorize]
public class LogisticsSuppliesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogisticsSuppliesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.LogisticsSupplyPost)]
    public async Task<Result> Post([FromBody] CreateLogisticsSupplyCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [SimaAuthorize(Permissions.LogisticsSupplyPut)]
    public async Task<Result> Put([FromBody] ModifyLogisticsSupplyCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.LogisticsSupplyDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteLogisticsSupplyCommand { Id = id };
        return await _mediator.Send(command);
    }
}