using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.ConfigurationAttributes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ConfigurationAttributes")]

public class ConfigurationAttributesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationAttributesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.ConfigurationAttributesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConfigurationAttributeByIdCommand { Id = id };
        return await _mediator.Send(command);
    }
}
