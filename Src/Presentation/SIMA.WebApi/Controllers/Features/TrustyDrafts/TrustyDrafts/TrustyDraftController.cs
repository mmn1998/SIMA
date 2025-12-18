using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Services.BehsazanServices.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.TrustyDrafts;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/TrustyDrafts")]
[Authorize]
public class TrustyDraftController : ControllerBase
{
    private readonly IMediator _mediator;

    public TrustyDraftController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("trustyDraftInquiry")]
    [SimaAuthorize(Permissions.TrustyDraftInquiry)]
    public async Task<Result> TrustyDraftInquiry([FromBody] TrustCurrencyDraft command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost]
    [SimaAuthorize(Permissions.TrustyDraftsPost)]
    public async Task<Result> Post([FromBody] CreateTrustyDraftCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [SimaAuthorize(Permissions.TrustyDraftsPut)]
    public async Task<Result> Put([FromBody] ModifyTrustyDraftCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.TrustyDraftsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteTrustyDraftCommand { Id = id };
        return await _mediator.Send(command);
    }

}