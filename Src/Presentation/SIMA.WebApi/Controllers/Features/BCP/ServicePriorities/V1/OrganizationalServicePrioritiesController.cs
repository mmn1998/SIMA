using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ServicePriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ServicePriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "OrganizationalServicePriorities")]
public class OrganizationalServicePrioritiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrganizationalServicePrioritiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateOrganizationalServicePriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyOrganizationalServicePriorityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteOrganizationalServicePriorityCommand { Id = id };
        return await _mediator.Send(command);
    }
}