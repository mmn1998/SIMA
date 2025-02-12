using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceIntensions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceIntensions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ConsequenceIntensions")]
public class ConsequenceIntensionsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceIntensionsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceIntensionQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllConsequenceIntensionsQuery query)
    {
        return await _mediator.Send(query);
    }
}