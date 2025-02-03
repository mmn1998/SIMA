using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.Products;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Products.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Products")]
public class ProductsController :ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.ProductsPost)] 
    public async Task<Result> Post([FromBody] CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
     //[SimaAuthorize(Permissions.ProductsPut)] 
    public async Task<Result> Put([FromBody] ModifyProductCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    /*
    [SimaAuthorize(Permissions.ProductsDelete)] 
    */
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteProductCommand { Id = id };
        return await _mediator.Send(command);
    }
}
