using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetPhysicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetPhysicalStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetPhysicalStatuses")]
public class AssetPhysicalStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetPhysicalStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AssetPhysicalStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAssetPhysicalStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AssetPhysicalStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllAssetPhysicalStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}