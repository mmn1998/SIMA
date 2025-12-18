using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryPointObjectives;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/RecoveryPointObjectives")]
public class RecoveryPointObjectivesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryPointObjectivesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.recoveryPointObjectivesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRecoveryPointObjectiveQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.recoveryPointObjectivesGetAll)]
    public async Task<Result> Get([FromBody] GetAllRecoveryPointObjectivesQuery query)
    {
        return await _mediator.Send(query);
    }
}