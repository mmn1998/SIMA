using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.ServicePriorities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ServicePriorities.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "OrganizationalServicePriorities")]
public class OrganizationalServicePrioritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrganizationalServicePrioritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetOrganizationalServicePriorityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllOrganizationalServicePrioritiesQuery query)
    {
        return await _mediator.Send(query);
    }
}