using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetTypes")]
public class AssetTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.AssetTypePost)]
    public async Task<Result> Post([FromBody] CreateAssetTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.AssetTypePut)]
    public async Task<Result> Put([FromBody] ModifyAssetTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AssetTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAssetTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}