using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceUserTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceUserTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceUserTypes")]
public class ServiceUserTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceUserTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceUserTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    public async Task<Result> Get([FromQuery] GetAllServiceUserTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}