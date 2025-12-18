using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.SeverityValues;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.SeverityValues.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/SeverityValues")]
public class SeverityValuesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SeverityValuesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.SeverityValueGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSeverityValueQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.SeverityValueGetAll)]
    public async Task<Result> Get([FromBody] GetAllSeverityValuesQuery query)
    {
        return await _mediator.Send(query);
    }
}