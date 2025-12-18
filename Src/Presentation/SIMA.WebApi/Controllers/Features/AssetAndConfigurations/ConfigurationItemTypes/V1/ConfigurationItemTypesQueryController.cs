using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemTypes")]
public class ConfigurationItemTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConfigurationItemTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ConfigurationItemTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllConfigurationItemTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}