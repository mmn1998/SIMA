using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftIssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftIssueTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftIssueTypes")]
public class DraftIssueTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftIssueTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftIssueTypesPost)]
    public async Task<Result> Post([FromBody] CreateDraftIssueTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftIssueTypesPut)]
    public async Task<Result> Put([FromBody] ModifyDraftIssueTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftIssueTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftIssueTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}