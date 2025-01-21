using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftReviewResults;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftReviewResults.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftReviewResults")]
public class DraftReviewResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftReviewResultsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftReviewResultsPost)]
    public async Task<Result> Post([FromBody] CreateDraftReviewResultCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftReviewResultsPut)]
    public async Task<Result> Put([FromBody] ModifyDraftReviewResultCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftReviewResultsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftReviewResultCommand { Id = id };
        return await _mediator.Send(command);
    }
}