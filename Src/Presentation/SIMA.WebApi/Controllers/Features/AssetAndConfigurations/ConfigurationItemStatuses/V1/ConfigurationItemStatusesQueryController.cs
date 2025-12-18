using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemStatuses")]
public class ConfigurationItemStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConfigurationItemStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ConfigurationItemStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllConfigurationItemStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}