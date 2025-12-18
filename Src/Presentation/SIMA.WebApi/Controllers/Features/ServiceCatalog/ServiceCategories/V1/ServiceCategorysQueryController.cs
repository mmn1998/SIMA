using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceCategories")]
public class ServiceCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ServiceCategoryGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceCategoryQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ServiceCategoryGetAll)]
    public async Task<Result> Get([FromBody] GetAllServiceCategoriesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("ServiceCategoriesExceptThis/{id}")]
    [SimaAuthorize(Permissions.ServiceCategoryGetAll)]
    public async Task<Result> ServiceCategoriesExceptThis([FromRoute] long id)
    {
        var query = new GetAllServiceCategoriesExceptThisIdQuery { Id = id };
        return await _mediator.Send(query);
    }
}