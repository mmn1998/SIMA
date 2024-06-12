using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.PhoneTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PhoneTypes")]
[Authorize]

public class PhoneTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhoneTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.PhoneTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetPhoneTypeQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.PhoneTypesGetAll)]
    public async Task<Result> Get(GetAllPhoneTypesQuery request)
    {
        return await _mediator.Send(request);

    }
}
