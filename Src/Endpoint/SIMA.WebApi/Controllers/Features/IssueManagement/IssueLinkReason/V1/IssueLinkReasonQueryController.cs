using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueLinkReason.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssueLinkReasons")]
public class IssueLinkReasonQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueLinkReasonQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssueLinkReasonsGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetIssueLinkReasonQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.IssueLinkReasonsGetAll)]
    public async Task<Result> Get([FromQuery] GetAllIssueLinkReasonsQuery request)
    {
        return await _mediator.Send(request);
    }
}
