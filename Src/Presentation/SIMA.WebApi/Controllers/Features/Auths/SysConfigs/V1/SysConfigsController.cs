using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.SysConfigs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.SysConfigs.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SysConfigs")]

public class SysConfigsController : ControllerBase
{
    private readonly IMediator _mediator;
    public SysConfigsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.SysConfigsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSysConfigCommand { Id = id };
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.SysConfigsPost)]
    public async Task<Result> Post([FromBody] CreateSystemConfigurationCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
}
