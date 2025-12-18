using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.AffectedHistories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.AffectedHistories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/AffectedHistories")]
public class AffectedHistoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AffectedHistoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.AffectedHistoryPost)]
    public async Task<Result> Post([FromBody] CreateAffectedHistoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.AffectedHistoryPut)]
    public async Task<Result> Put([FromBody] ModifyAffectedHistoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.AffectedHistoryDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAffectedHistoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}