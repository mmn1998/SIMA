using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlowCompany.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkflowCompany")]
[Authorize]
public class WorkflowCompanyQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowCompanyQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.WorkFlowCompanyGet)]

    public async Task<Result> Get(long id)
    {
        var query = new GetWorkFlowCompanyQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.WorkFlowCompanyGetAll)]

    public async Task<Result> Get([FromQuery] GetAllWorkFlowCompanyQuery query)
    {
        var result = await _mediator.Send(query);
        return result;
    }
}
