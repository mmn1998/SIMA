using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.PlanResponsibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.PlanResponsibilities.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/PlanResponsibilities")]
public class PlanResponsibilitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanResponsibilitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.planResponsibilityPost)]
    public async Task<Result> Post([FromBody] CreatePlanResponsibilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.planResponsibilityPost)]
    public async Task<Result> Put([FromBody] ModifyPlanResponsibilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.planResponsibilityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePlanResponsibilityCommand { Id = id };
        return await _mediator.Send(command);
    }
}