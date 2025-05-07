using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.Consequences;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.Consequences;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/Consequences")]
public class ConsequencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequencesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.consequencesPost)]
    public async Task<Result> Post([FromBody] CreateConsequenceCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.consequencesPut)]
    public async Task<Result> Put([FromBody] ModifyConsequenceCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.consequencesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceCommand { Id = id };
        return await _mediator.Send(command);
    }
}