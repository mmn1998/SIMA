using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.AgentBankWageShareStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.AgentBankWageShareStatuses.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/AgentBankWageShareStatuses")]
[Authorize]
public class AgentBankWageShareStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AgentBankWageShareStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AgentBankWageShareStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAgentBankWageShareStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AgentBankWageShareStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllAgentBankWageShareStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}
