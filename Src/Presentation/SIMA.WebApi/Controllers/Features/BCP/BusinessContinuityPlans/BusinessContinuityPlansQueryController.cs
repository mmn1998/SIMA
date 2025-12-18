using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinessContinuityPlans;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BusinessContinuityPlans")]
[Authorize]
public class BusinessContinuityPlansQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessContinuityPlansQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.businessContinuityPlanGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBusinessContinuityPlanQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet("GetAllPlanVersioningsByPlanId/{planId}")]
    [SimaAuthorize(Permissions.businessContinuityVersioningsByPlanGet)]
    public async Task<Result> GetAllPlanVersioningsByPlanId([FromRoute] long planId)
    {
        var query = new GetAllPlanVersioningsByPlanIdQuery { BusinessContinuityPlanId = planId };
        return await _mediator.Send(query);
    }
    [HttpGet("GetAllPlanAssumptiongsByPlanId/{planId}")]
    [SimaAuthorize(Permissions.businessContinuityAssumptionsByPlanGet)]
    public async Task<Result> GetAllPlanAssumptionsByPlanId([FromRoute] long planId)
    {
        var query = new GetAllPlanAssumptionsByPlanIdQuery { BusinessContinuityPlanId = planId };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.businessContinuityPlanGetAll)]
    public async Task<Result> Get([FromBody] GetAllBusinessContinuityPlansQuery query)
    {
        return await _mediator.Send(query);
    }
}
