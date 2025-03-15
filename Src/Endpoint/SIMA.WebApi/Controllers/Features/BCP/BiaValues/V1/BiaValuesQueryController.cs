using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.BiaValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.BiaValues.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BiaValues")]
public class BiaValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BiaValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBiaValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllBiaValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}