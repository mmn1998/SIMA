using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.RecoveryOptionPriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryOptionPriorities.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/RecoveryOptionPriorities")]
public class RecoveryOptionPrioritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryOptionPrioritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.recoveryOptionPriorityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRecoveryOptionPriorityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.recoveryOptionPriorityGetAll)]
    public async Task<Result> Get([FromBody] GetAllRecoveryOptionPrioritiesQuery query)
    {
        return await _mediator.Send(query);
    }
}