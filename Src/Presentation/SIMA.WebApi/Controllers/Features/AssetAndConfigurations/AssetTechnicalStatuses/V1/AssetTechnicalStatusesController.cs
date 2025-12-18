using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTechnicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetTechnicalStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetTechnicalStatuses")]
public class AssetTechnicalStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTechnicalStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.AssetTechnicalStatusPost)]
    public async Task<Result> Post([FromBody] CreateAssetTechnicalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.AssetTechnicalStatusPut)]
    public async Task<Result> Put([FromBody] ModifyAssetTechnicalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AssetTechnicalStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAssetTechnicalStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}