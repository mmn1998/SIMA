using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.InherentOccurrenceProbabilityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/InherentOccurrenceProbabilityValues")]
public class InherentOccurrenceProbabilityValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InherentOccurrenceProbabilityValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityValueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetInherentOccurrenceProbabilityValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityValueGetAll)]
    public async Task<Result> Get([FromBody] GetAllInherentOccurrenceProbabilityValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}