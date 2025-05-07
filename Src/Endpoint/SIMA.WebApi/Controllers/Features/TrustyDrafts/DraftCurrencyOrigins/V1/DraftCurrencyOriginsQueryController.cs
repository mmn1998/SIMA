using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftCurrencyOrigins.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftCurrencyOrigins")]
public class DraftCurrencyOriginsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftCurrencyOriginsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.draftCurrencyOriginGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftCurrencyOriginQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.draftCurrencyOriginGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftCurrencyOriginsQuery query)
    {
        return await _mediator.Send(query);
    }
}