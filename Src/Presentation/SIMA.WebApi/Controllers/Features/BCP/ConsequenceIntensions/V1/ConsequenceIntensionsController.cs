using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceIntensions.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ConsequenceIntensions")]
public class ConsequenceIntensionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceIntensionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateConsequenceIntensionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyConsequenceIntensionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceIntensionCommand { Id = id };
        return await _mediator.Send(command);
    }
}