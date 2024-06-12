using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssuePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssuePrioroties.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssuePriorities")]
public class IssuePrioritiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssuePrioritiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.IssuePriorotiesGetAll)]
    public async Task<Result> Get(GetAllIssuePriorotiesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssuePriorotiesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetIssuePriorotyQuery { Id = id };
        return await _mediator.Send(query);
    }
}
