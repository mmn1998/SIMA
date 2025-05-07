using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinesImpactAnalysises.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BusinessImpactAnalysises")]
public class BusinessImpactAnalysisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessImpactAnalysisesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.businessImpactAnalysisPost)]
    public async Task<Result> Post([FromBody] CreateBusinessImpactAnalysisCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.businessImpactAnalysisPut)]
    public async Task<Result> Put([FromBody] ModifyBusinessImpactAnalysisCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.businessImpactAnalysisDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBusinessImpactAnalysisCommand { Id = id };
        return await _mediator.Send(command);
    }
}