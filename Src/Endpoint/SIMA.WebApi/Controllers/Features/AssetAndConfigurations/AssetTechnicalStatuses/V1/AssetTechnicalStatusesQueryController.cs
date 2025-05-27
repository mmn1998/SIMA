using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.AssetTechnicalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.AssetTechnicalStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/AssetTechnicalStatuses")]
public class AssetTechnicalStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetTechnicalStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AssetTechnicalStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAssetTechnicalStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AssetTechnicalStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllAssetTechnicalStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}