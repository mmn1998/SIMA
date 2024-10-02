using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.CriticalActivities.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CriticalActivities")]
public class CriticalActivitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CriticalActivitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.CriticalActivityGetAll)]
    public async Task<Result> Get([FromBody] GetAllCriticalActivitiesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}/{issueId}")]
    [SimaAuthorize(Permissions.CriticalActivityGet)]
    public async Task<Result> Get([FromRoute] long id, [FromRoute] long issueId)
    {
        var query = new GetCriticalActivityQuery { Id = id, IssueId = issueId };
        return await _mediator.Send(query);
    }
}