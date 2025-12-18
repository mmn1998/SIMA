using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.CriticalActivities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.CriticalActivities.V1;

[Route("serviceCatalog/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CriticalActivities")]
public class CriticalActivitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CriticalActivitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.CriticalActivityPost)]
    public async Task<Result> Post([FromBody] CreateCriticalActivityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.CriticalActivityPut)]
    public async Task<Result> Put([FromBody] ModifyCriticalActivityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.CriticalActivityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCriticalActivityCommand { Id = id };
        return await _mediator.Send(command);
    }
}