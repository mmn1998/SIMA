using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.CurrentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CurrentOccurrenceProbabilityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CurrentOccurrenceProbabilityValues")]
public class CurrentOccurrenceProbabilityValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrentOccurrenceProbabilityValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityValueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCurrentOccurrenceProbabilityValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.CurrentOccurrenceProbabilityValueGetAll)]
    public async Task<Result> Get([FromBody] GetAllCurrentOccurrenceProbabilityValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}