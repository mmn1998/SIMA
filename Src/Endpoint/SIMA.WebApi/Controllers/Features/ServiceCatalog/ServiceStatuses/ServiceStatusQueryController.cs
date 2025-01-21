using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceStatuses;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceStatus")]
[Authorize]
public class ServiceStatusQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceStatusQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ServiceStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ServiceStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllServiceStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}
