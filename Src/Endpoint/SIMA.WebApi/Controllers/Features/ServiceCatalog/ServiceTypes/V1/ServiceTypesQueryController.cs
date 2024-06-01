using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceTypes")]
public class ServiceTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    public async Task<Result> Get([FromQuery] GetAllServiceTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}