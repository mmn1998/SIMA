using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemTypes")]
public class ConfigurationItemTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ConfigurationItemTypePost)]
    public async Task<Result> Post([FromBody] CreateConfigurationItemTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ConfigurationItemTypePut)]
    public async Task<Result> Put([FromBody] ModifyConfigurationItemTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConfigurationItemTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}