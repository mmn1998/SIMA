using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemRelationshipTypes")]
public class ConfigurationItemRelationshipTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemRelationshipTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemRelationshipTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConfigurationItemRelationshipTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ConfigurationItemRelationshipTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllConfigurationItemRelationshipTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}