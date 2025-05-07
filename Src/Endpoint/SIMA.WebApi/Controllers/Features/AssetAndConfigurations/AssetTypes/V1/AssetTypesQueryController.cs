using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetTypes")]
public class AssetTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AssetTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAssetTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AssetTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllAssetTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}