using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftOrigins.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftOrigins")]
public class DraftOriginsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftOriginsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftOriginsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftOriginQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftOriginsGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftOriginsQuery query)
    {
        return await _mediator.Send(query);
    }
}