using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.LoactionTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LocationTypes")]
[Authorize]

public class LocationTypesQueryController : ControllerBase
{

    private readonly IMediator _mediator;
    public LocationTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.LocationTypeGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetLocationTypeQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.LocationTypeGetAll)]
    public async Task<Result> Get(GetAllLocationTypeQuery request)
    {
        return await _mediator.Send(request);
    }
}
