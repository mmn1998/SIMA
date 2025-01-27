using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.InherentOccurrenceProbabilityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/InherentOccurrenceProbabilityValues")]
public class InherentOccurrenceProbabilityValuesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InherentOccurrenceProbabilityValuesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityValuePost)]
    public async Task<Result> Post([FromBody] CreateInherentOccurrenceProbabilityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityValuePut)]
    public async Task<Result> Put([FromBody] ModifyInherentOccurrenceProbabilityValueCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityValueDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteInherentOccurrenceProbabilityValueCommand { Id = id };
        return await _mediator.Send(command);
    }
}