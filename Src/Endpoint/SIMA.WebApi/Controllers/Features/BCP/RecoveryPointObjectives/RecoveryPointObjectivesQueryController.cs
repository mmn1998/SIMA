using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.RecoveryPointObjectives;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.RecoveryPointObjectives;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RecoveryPointObjectives")]
public class RecoveryPointObjectivesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecoveryPointObjectivesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetRecoveryPointObjectiveQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllRecoveryPointObjectivesQuery query)
    {
        return await _mediator.Send(query);
    }
}