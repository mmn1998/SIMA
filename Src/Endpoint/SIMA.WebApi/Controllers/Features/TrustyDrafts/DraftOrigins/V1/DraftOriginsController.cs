using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.V1.DraftOrigins;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftOrigins")]
public class DraftOriginsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftOriginsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftOriginsPost)]
    public async Task<Result> Post([FromBody] CreateDraftOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftOriginsPut)]
    public async Task<Result> Put([FromBody] ModifyDraftOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftOriginsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftOriginCommand { Id = id };
        return await _mediator.Send(command);
    }
}