using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.Suppliers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.Suppliers.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Suppliers")]
public class SuppliersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSupplierQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllSuppliersQuery query)
    {
        return await _mediator.Send(query);
    }
}