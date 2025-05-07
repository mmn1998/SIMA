using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlowActors.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkflowActors")]
[Authorize]

public class WorkflowActorsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowActorsQueryController(IMediator mediator)
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

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.WorkFlowActorGetAll)]
    public async Task<Result> Get(GetAllWorkFlowActorsQuery query)
    {
        return await _mediator.Send(query);
    }
    // Todo hossein add a permission for employee
    [HttpGet("GetEmployee/{id}")]
    [SimaAuthorize(Permissions.WorkFlowActorGetAll)]
    public async Task<Result<IEnumerable<GetWorkflowActorEmployeeQueryResult>>> GetEmployee(long id)
    {
        var query = new GetWorkflowActorEmployeeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
