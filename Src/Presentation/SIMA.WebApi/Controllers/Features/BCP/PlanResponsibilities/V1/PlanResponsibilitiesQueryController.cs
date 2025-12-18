using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.PlanResponsibilities.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/PlanResponsibilities")]
public class PlanResponsibilitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanResponsibilitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.planResponsibilityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPlanResponsibilityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.planResponsibilityGetAll)]
    public async Task<Result> Get([FromBody] GetAllPlanResponsibilitiesQuery query)
    {
        return await _mediator.Send(query);
    }
}