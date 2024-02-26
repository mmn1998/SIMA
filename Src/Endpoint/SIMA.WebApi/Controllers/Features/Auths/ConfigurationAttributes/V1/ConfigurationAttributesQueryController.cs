using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.ConfigurationAttributes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ConfigurationAttributes")]

public class ConfigurationAttributesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigurationAttributesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ConfigurationAttributesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConfigurationAttributeQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet]
    [SimaAuthorize(Permissions.ConfigurationAttributesGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllConfigurationAttributes { Request = request };
        return await _mediator.Send(query);
    }
}

