using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.SolutionPriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.SolutionPeriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SolutionPriorities")]
public class SolutionPrioritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SolutionPrioritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSolutionPriorityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllSolutionPrioritiesQuery query)
    {
        return await _mediator.Send(query);
    }
}