using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.ConfigurationItems;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.ConfigurationItems.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/ConfigurationItems")]
[Authorize]
public class ConfigurationItemsController : Controller
{
    private readonly IMediator _mediator;

    public ConfigurationItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.ConfigurationItemPost)]
    public async Task<Result> Post([FromBody] CreateConfigurationItemCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.ConfigurationItemPut)]
    public async Task<Result> Put([FromBody] ModifyConfigurationItemCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ConfigurationItemDelete)]
    public async Task<Result> Put([FromRoute] long id)
    {
        var command = new DeleteConfigurationItemCommand { Id = id };
        return await _mediator.Send(command);
    }
    
}
