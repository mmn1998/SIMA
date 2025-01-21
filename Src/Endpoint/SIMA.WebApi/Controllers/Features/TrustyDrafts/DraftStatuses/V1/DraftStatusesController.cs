using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftStatuses")]
public class DraftStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftStatusesPost)]
    public async Task<Result> Post([FromBody] CreateDraftStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftStatusesPut)]
    public async Task<Result> Put([FromBody] ModifyDraftStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftStatusesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}