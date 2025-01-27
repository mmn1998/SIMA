using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CurrentOccurrenceProbabilities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CurrentOccurrenceProbabilities")]
public class CurrentOccurrenceProbabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrentOccurrenceProbabilitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityPost)]
    public async Task<Result> Post([FromBody] CreateCurrentOccurrenceProbabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityPut)]
    public async Task<Result> Put([FromBody] ModifyCurrentOccurrenceProbabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCurrentOccurrenceProbabilityCommand { Id = id };
        return await _mediator.Send(command);
    }
}