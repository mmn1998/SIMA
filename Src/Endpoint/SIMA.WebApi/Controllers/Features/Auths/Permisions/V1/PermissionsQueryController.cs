using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Permission;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Permisions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Permisions")]
[Authorize]

//
public class PermissionsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.PermisionsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPermissionQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.PermisionsGetAll)] /// TODO Mehdi
                                                  /// MEHDI : it doesnt have any wireframes !!!
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllPermissionsQuery { Request = request };
        var result = await _mediator.Send(query);
        return result;
    }
    [HttpGet("GetPermissionsByDomainId")]
    [AllowAnonymous]
    public async Task<Result> Get([FromQuery] int domainId)
    {
        var query = new GetAllPermissionsByDomainIdQuery
        {
            DomainId = domainId
        };
        var result = await _mediator.Send(query);
        return result;
    }
}
