using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Assets;
using SIMA.Application.Contract.Features.AssetAndConfigurations.AssetTechnicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Assets.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Assets")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [SimaAuthorize(Permissions.AssetPost)]
    public async Task<Result> Post([FromBody] CreateAssetCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.AssetPut)]
    public async Task<Result> Put([FromBody] ModifyAssetCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AssetDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAssetCommand { Id = id };
        return await _mediator.Send(command);
    }
    
    
}