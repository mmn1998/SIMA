using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Assets;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Assets.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Assets")]
//[Authorize]
public class AssetsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AssetGetAll)]
    public async Task<Result> Get([FromBody] GetAllAssetsQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("GetByCode/{code}")]
    [SimaAuthorize(Permissions.AssetGet)]
    public async Task<Result> Get([FromRoute] string code)
    {
        var query = new GetAssetByCodeQuery { Code = code };
        return await _mediator.Send(query);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AssetGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAssetByIdQuery { Id = id };
        return await _mediator.Send(query);
    }


    [HttpGet("GetCombo")]
    public async Task<Result> GetCombo()
    {
        var query = new GetAssetComboQuery();
        return await _mediator.Send(query);
    }
}