using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.AgentBankWageShareStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/AgentBankWageShareStatuses")]
[Authorize]
public class AgentBankWageShareStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgentBankWageShareStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.AgentBankWageShareStatusPost)]
    public async Task<Result> Post([FromBody] CreateAgentBankWageShareStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.AgentBankWageShareStatusPut)]
    public async Task<Result> Put([FromBody] ModifyAgentBankWageShareStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AgentBankWageShareStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAgentBankWageShareStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}
