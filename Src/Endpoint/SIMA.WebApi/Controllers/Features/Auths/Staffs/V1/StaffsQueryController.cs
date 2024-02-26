using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Staffs.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Staffs")]
[Authorize]

public class StaffsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [SimaAuthorize(Permissions.StaffsGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllStaffQuery { Request = request };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.StaffsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetStaffQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }
}
