using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServiceCustomerTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceCustomerTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ServiceCustomerTypes")]
public class ServiceCustomerTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceCustomerTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetServiceCustomerTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    public async Task<Result> Get([FromQuery] GetAllServiceCustomerTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}