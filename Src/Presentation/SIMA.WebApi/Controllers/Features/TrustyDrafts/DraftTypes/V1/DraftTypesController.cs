using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.DraftTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.DraftTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/DraftTypes")]
public class DraftTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DraftTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DraftTypesPost)]
    public async Task<Result> Post([FromBody] CreateDraftTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DraftTypesPut)]
    public async Task<Result> Put([FromBody] ModifyDraftTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DraftTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDraftTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}