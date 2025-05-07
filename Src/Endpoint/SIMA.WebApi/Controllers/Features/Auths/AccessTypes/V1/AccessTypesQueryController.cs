using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.AccessTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.AccessTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "AccessTypes")]
[Authorize]
public class AccessTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccessTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllAccessTypesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAccessTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}