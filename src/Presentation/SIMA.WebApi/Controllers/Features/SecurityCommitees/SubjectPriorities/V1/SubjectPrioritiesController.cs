using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.SubjectPriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SubjectPriorities")]
public class SubjectPrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectPrioritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateSubjectPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifySubjectPriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSubjectPriorityCommand { Id = id };
        return await _mediator.Send(command);
    }
}
