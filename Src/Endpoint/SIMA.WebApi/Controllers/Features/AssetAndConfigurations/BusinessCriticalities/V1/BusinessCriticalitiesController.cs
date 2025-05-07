using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.BusinessCriticalities.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/BusinessCriticalities")]
public class BusinessCriticalitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessCriticalitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.BusinessCriticalityPost)]
    public async Task<Result> Post([FromBody] CreateBusinessCriticalityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.BusinessCriticalityPut)]
    public async Task<Result> Put([FromBody] ModifyBusinessCriticalityCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.BusinessCriticalityDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBusinessCriticalityCommand { Id = id };
        return await _mediator.Send(command);
    }
}