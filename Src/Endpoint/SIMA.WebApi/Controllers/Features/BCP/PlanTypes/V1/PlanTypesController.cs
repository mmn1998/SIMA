using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.PlanTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.PlanTypes.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/PlanTypes")]
public class PlanTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreatePlanTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyPlanTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePlanTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}