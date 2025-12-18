using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetPhysicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetPhysicalStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetPhysicalStatuses")]
public class AssetPhysicalStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetPhysicalStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.AssetPhysicalStatusPost)]
    public async Task<Result> Post([FromBody] CreateAssetPhysicalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.AssetPhysicalStatusPut)]
    public async Task<Result> Put([FromBody] ModifyAssetPhysicalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AssetPhysicalStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAssetPhysicalStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}