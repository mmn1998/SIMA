using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.UserTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.UserTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UserTypes")]
public class UserTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.userTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetUserTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.userTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllUserTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}