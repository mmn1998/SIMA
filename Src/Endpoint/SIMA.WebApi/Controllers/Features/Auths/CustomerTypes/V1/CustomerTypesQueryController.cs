using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.CustomerTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.CustomerTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CustomerTypes")]
public class CustomerTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCustomerTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllCustomerTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}