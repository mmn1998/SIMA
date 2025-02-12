using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.SolutionPriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.SolutionPeriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SolutionPriorities")]
public class SolutionPrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SolutionPrioritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateSolutionPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifySolutionPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSolutionPriorityCommand { Id = id };
        return await _mediator.Send(command);
    }
}