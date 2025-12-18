using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.ConsequenceValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.ConsequenceValues.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/ConsequenceValues")]
public class ConsequenceValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllConsequenceValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}