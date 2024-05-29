using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssueTypes")]
public class IssueTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [SimaAuthorize(Permissions.IssueTypesGetAll)]
    public async Task<Result> Get([FromQuery] GetAllIssueTypesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssueTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetIssueTypesQuery { Id = id };
        return await _mediator.Send(query);
    }
}
