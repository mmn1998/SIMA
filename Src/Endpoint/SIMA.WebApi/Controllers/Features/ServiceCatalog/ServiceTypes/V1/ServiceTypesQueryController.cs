using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceTypes.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceTypes")]
[Authorize]
public class ServiceTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.ServiceTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.ServiceTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllServiceTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}
