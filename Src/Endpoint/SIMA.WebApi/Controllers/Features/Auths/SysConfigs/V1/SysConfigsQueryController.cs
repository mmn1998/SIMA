using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.SysConfigs;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.SysConfigs.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SysConfigs")]

public class SysConfigsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SysConfigsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GenderGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSysConfigQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GenderGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllSysConfigQuery { Request = request };
        var result = await _mediator.Send(query);
        return result;
    }
}
