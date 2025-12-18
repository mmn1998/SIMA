using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssuePrioroties.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssuePriorities")]
public class IssuePrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssuePrioritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.IssuePriorotiesPost)]
    public async Task<Result> Post([FromBody] CreateIssuePriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.IssuePriorotiesPut)]
    public async Task<Result> Put([FromBody] ModifyIssuePriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.IssuePriorotiesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteIssuePriorityCommand
        {
            Id = id
        };
        return await _mediator.Send(command);
    }
}
