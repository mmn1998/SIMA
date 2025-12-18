using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftValorStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftValorStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftValorStatuses")]
public class DraftValorStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftValorStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftValorStatusesPost)]
    public async Task<Result> Post([FromBody] CreateDraftValorStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftValorStatusesPut)]
    public async Task<Result> Put([FromBody] ModifyDraftValorStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftValorStatusesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftValorStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}