using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.RequestValors.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/RequestValors")]
public class RequestValorsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestValorsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.RequestValorsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRequestValorQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.RequestValorsGetAll)]
    public async Task<Result> Get([FromBody] GetAllRequestValorsQuery query)
    {
        return await _mediator.Send(query);
    }
}