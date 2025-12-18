using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ConsequenceValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceValues.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ConsequenceValues")]
public class ConsequenceValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateConsequenceValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyConsequenceValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}