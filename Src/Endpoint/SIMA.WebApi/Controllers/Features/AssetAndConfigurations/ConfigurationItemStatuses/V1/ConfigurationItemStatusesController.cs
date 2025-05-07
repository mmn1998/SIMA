using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemStatuses")]
public class ConfigurationItemStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ConfigurationItemStatusPost)]
    public async Task<Result> Post([FromBody] CreateConfigurationItemStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ConfigurationItemStatusPut)]
    public async Task<Result> Put([FromBody] ModifyConfigurationItemStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConfigurationItemStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}