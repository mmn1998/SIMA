using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftDestinations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftDestinations;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftDestinations")]
[Authorize]
public class DraftDestinationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftDestinationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.draftDestinationPost)]
    public async Task<Result> Post([FromBody] CreateDraftDestinationCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.draftDestinationPut)]
    public async Task<Result> Put([FromBody] ModifyDraftDestinationCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.draftDestinationDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftDestinationCommand { Id = id };
        return await _mediator.Send(command);
    }
}
