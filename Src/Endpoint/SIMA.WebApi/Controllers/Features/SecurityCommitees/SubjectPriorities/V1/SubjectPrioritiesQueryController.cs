using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.SecurityCommitees.SubjectPriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.SecurityCommitees.SubjectPriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SubjectPriorities")]
public class SubjectPrioritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectPrioritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllSubjectPrioritiesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSubjectPriorityQuery { Id = id };
        return await _mediator.Send(query);
    }
}
