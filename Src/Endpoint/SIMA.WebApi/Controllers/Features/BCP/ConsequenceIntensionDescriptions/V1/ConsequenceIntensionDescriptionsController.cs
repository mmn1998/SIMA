using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceIntensionDescriptions.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ConsequenceIntensionDescriptions")]
public class ConsequenceIntensionDescriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceIntensionDescriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateConsequenceIntensionDescriptionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyConsequenceIntensionDescriptionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceIntensionDescriptionCommand { Id = id };
        return await _mediator.Send(command);
    }
}