using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.CancellationResaons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.CancellationResaons.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/CancellationResaons")]
public class CancellationResaonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CancellationResaonsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.cancellationResaonPost)]
    public async Task<Result> Post([FromBody] CreateCancellationResaonCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.cancellationResaonPut)]
    public async Task<Result> Put([FromBody] ModifyCancellationResaonCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.cancellationResaonDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCancellationResaonCommand { Id = id };
        return await _mediator.Send(command);
    }
}