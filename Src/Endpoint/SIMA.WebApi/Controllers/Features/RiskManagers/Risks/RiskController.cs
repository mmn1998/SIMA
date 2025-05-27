using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Risks;


[Route("riskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Risks")]
public class RisksController : ControllerBase
{
    private readonly IMediator _mediator;
    public RisksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    //[SimaAuthorize(Permissions.RisksPost)]
    public async Task<Result> Post([FromBody] CreateRiskCommand command)
    {
        try
        {
            return await _mediator.Send(command);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    [HttpPut]
    [SimaAuthorize(Permissions.RisksPut)]
    public async Task<Result> Put([FromBody] ModifyRiskCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.RisksDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRiskCommand { Id = id };
        return await _mediator.Send(command);
    }
}