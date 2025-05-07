using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.AffectedHistories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.AffectedHistories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/AffectedHistories")]
public class AffectedHistoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AffectedHistoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.AffectedHistoryGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAffectedHistoryQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.AffectedHistoryGetAll)]
    public async Task<Result> Get([FromBody] GetAllAffectedHistoriesQuery query)
    {
        return await _mediator.Send(query);
    }
}