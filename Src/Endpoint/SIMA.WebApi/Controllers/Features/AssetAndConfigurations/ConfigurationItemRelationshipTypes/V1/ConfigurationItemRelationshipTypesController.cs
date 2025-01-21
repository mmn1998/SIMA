using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItemRelationshipTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItemRelationshipTypes")]
public class ConfigurationItemRelationshipTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationItemRelationshipTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateConfigurationItemRelationshipTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyConfigurationItemRelationshipTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConfigurationItemRelationshipTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}