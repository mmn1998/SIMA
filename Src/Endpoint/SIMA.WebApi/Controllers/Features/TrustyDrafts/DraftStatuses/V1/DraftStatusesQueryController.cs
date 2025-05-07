using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftStatuses")]
public class DraftStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftStatusesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftStatusesGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}