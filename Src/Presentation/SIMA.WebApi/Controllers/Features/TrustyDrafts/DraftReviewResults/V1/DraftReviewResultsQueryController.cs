using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftReviewResults.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftReviewResults")]
public class DraftReviewResultsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftReviewResultsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DraftReviewResultsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDraftReviewResultQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DraftReviewResultsGetAll)]
    public async Task<Result> Get([FromBody] GetAllDraftReviewResultsQuery query)
    {
        return await _mediator.Send(query);
    }
}