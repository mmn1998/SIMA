using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftCurrencyOrigins;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftCurrencyOrigins.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftCurrencyOrigins")]
public class DraftCurrencyOriginsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftCurrencyOriginsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.draftCurrencyOriginPost)]
    public async Task<Result> Post([FromBody] CreateDraftCurrencyOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.draftCurrencyOriginPut)]
    public async Task<Result> Put([FromBody] ModifyDraftCurrencyOriginCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.draftCurrencyOriginDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftCurrencyOriginCommand { Id = id };
        return await _mediator.Send(command);
    }
}