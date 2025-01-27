using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.EvaluationCriterias;
using SIMA.Application.Contract.Features.RiskManagers.Frequencies;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Frequencies.V1;

[ApiController]
[Route("riskManagement/[controller]")]
[Authorize]
[ApiExplorerSettings(GroupName = "RiskManagment/Frequency")]

public class FrequenciesController
{
    private readonly IMediator _mediator;

    public FrequenciesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPost)]
    */
    public async Task<Result> Post([FromBody] CreateFrequencyCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaPut)]
    */
    public async Task<Result> Put([FromBody] ModifyFrequencyCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    /*[SimaAuthorize(Permissions.EvaluationCriteriaDelete)]*/
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteFrequencyCommand { Id = id };
        return await _mediator.Send(command);
    }
    
}