using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceBoundles;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceBoundles.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceBoundles")]
public class ServiceBoundlesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceBoundlesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceBoundleQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllServiceBoundlesQuery query)
    {
        return await _mediator.Send(query);
    }
}
