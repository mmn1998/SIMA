using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.CurrentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CurrentOccurrenceProbabilityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CurrentOccurrenceProbabilityValues")]
public class CurrentOccurrenceProbabilityValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrentOccurrenceProbabilityValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityValuePost)]
    public async Task<Result> Post([FromBody] CreateCurrentOccurrenceProbabilityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityValuePut)]
    public async Task<Result> Put([FromBody] ModifyCurrentOccurrenceProbabilityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityValueDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCurrentOccurrenceProbabilityValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}