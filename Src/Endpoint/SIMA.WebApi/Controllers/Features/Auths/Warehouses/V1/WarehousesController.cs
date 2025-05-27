using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Warehouses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Warehouses.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Warehouses")]
public class WarehousesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehousesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.WarehousePost)]
    public async Task<Result> Post([FromBody] CreateWarehouseCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.WarehousePut)]
    public async Task<Result> Put([FromBody] ModifyWarehouseCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.WarehouseDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteWarehouseCommand { Id = id };
        return await _mediator.Send(command);
    }
}