using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensionDescriptions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceIntensionDescriptions.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ConsequenceIntensionDescriptions")]
public class ConsequenceIntensionDescriptionsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceIntensionDescriptionsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceIntensionDescriptionQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllConsequenceIntensionDescriptionsQuery query)
    {
        return await _mediator.Send(query);
    }
}