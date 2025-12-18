using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.Origins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.Origins.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/Origins")]
public class OriginsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public OriginsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.originGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetOriginQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.originGetAll)]
    public async Task<Result> Get([FromBody] GetAllOriginsQuery query)
    {
        return await _mediator.Send(query);
    }
}