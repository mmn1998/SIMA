using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftDestinations;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftDestinations")]
[Authorize]
public class DraftDestinationQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftDestinationQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.draftDestinationGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftDestinationQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.draftDestinationGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftDestinationsQuery query)
    {
        return await _mediator.Send(query);
    }
}
