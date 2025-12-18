using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.InherentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.InherentOccurrenceProbabilities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagement/InherentOccurrenceProbabilities")]
public class InherentOccurrenceProbabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InherentOccurrenceProbabilitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityPost)]
    public async Task<Result> Post([FromBody] CreateInherentOccurrenceProbabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityPut)]
    public async Task<Result> Put([FromBody] ModifyInherentOccurrenceProbabilityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteInherentOccurrenceProbabilityCommand { Id = id };
        return await _mediator.Send(command);
    }
}