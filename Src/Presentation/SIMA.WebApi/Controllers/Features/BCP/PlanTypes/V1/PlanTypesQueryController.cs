using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.PlanTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.PlanTypes.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/PlanTypes")]
public class PlanTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPlanTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllPlanTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}