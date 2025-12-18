using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.ReconsilationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.ReconsilationTypes.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/ReconciliationTypes")]
public class ReconciliationTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReconciliationTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ReconciliationTypesPost)]
    public async Task<Result> Post([FromBody] CreateReconsilationTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ReconciliationTypesPut)]
    public async Task<Result> Put([FromBody] ModifyReconsilationTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ReconciliationTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteReconsilationTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}