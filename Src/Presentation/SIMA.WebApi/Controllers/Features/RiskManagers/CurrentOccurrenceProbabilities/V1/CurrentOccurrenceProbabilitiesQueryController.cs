using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CurrentOccurrenceProbabilities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CurrentOccurrenceProbabilities")]
public class CurrentOccurrenceProbabilitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrentOccurrenceProbabilitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCurrentOccurrenceProbabilityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityGetAll)]
    public async Task<Result> Get([FromBody] GetAllCurrentOccurrenceProbabilitiesQuery query)
    {
        return await _mediator.Send(query);
    }
}