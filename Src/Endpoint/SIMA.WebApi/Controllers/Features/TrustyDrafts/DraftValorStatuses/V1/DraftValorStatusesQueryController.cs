using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftValorStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftValorStatuses")]
public class DraftValorStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftValorStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftValorStatusesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftValorStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftValorStatusesGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftValorStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}