using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlowActors.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkFlowActors")]
[Authorize]

public class WorkFlowActorsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkFlowActorsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.WorkFlowActorGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetWorkFlowActorQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.WorkFlowActorGetAll)]
    public async Task<Result> Get([FromQuery] GetAllWorkFlowActorsQuery request)
    {
        return await _mediator.Send(request);
    }
}
