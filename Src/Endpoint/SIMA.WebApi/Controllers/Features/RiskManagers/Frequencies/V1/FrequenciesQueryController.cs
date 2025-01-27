using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.EvaluationCriterias;
using SIMA.Application.Query.Contract.Features.RiskManagement.Frequencies;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Frequencies.V1;

public class FrequenciesQueryController
{
    private readonly IMediator _mediator;

    public FrequenciesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGet)]
    */
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetFrequencyQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    /*
    [SimaAuthorize(Permissions.EvaluationCriteriaGetAll)]
    */
    public async Task<Result> Get([FromBody] GetAllFrequenciesQuery query)
    {
        return await _mediator.Send(query);
    }
}