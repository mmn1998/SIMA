using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.Services;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.Services.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Services")]
//[Authorize]

public class ServicesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServicesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.ServiceGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ServiceGetAll)]
    public async Task<Result> Get([FromBody] GetAllServicesQuery query)
    {
        return await _mediator.Send(query);
    }
}