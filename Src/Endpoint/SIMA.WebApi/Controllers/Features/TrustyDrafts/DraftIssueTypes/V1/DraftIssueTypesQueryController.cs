using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftIssueTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftIssueTypes")]
public class DraftIssueTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftIssueTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftIssueTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftIssueTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftIssueTypesGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftIssueTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}