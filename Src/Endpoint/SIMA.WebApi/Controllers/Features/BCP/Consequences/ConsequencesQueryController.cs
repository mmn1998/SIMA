using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.Consequences;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.Consequences;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Consequences")]
public class ConsequencesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequencesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllConsequencesQuery query)
    {
        return await _mediator.Send(query);
    }
}