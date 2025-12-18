using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Products;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Products.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Products")]
[Authorize]
public class ProductsQueryController :ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ProductsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetProductQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet("GetByCode")]
    [SimaAuthorize(Permissions.ProductsGet)]
    public async Task<Result> Get([FromQuery] string code)
    {
        var query = new GetProductByCodeQuery { Code = code };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.ProductsGetAll)]
    public async Task<Result> Get([FromBody] GetAllProductQuery query)
    {
        return await _mediator.Send(query);
    }
}
