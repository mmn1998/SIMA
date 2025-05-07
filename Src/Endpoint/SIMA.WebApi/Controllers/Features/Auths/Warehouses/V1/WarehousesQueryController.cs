using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Warehouses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Warehouses.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Warehouses")]
public class WarehousesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehousesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.WarehouseGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetWarehouseQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.WarehouseGetAll)]
    public async Task<Result> Get([FromBody] GetAllWarehousesQuery query)
    {
        return await _mediator.Send(query);
    }
}