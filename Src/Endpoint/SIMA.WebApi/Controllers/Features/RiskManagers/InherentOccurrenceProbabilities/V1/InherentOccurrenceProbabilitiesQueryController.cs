using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.InherentOccurrenceProbabilities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.InherentOccurrenceProbabilities.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/InherentOccurrenceProbabilities")]
public class InherentOccurrenceProbabilitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InherentOccurrenceProbabilitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetInherentOccurrenceProbabilityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.InherentOccurrenceProbabilityGetAll)]
    public async Task<Result> Get([FromBody] GetAllInherentOccurrenceProbabilitiesQuery query)
    {
        return await _mediator.Send(query);
    }
}