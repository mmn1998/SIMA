using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Loactions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Locations")]
[Authorize]

public class LocationsQueryController : ControllerBase
{
    private readonly IMediator _mediator;
    public LocationsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.LocationGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetLocationQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.LocationGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        try
        {
            var query = new GetAllLocationQuery { Request = request };
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception e)
        {

            throw;
        }
    }
    [HttpGet("GetParentLocationsByLocationTypeId")]
    [SimaAuthorize(Permissions.GetParentLocationsByLocationTypeId)]
    public async Task<Result> Get([FromQuery] int locationTypeId)
    {
        var query = new GetParentLocationsByLocationTypeIdQuery
        {
            LocationTypeId = locationTypeId
        };
        return await _mediator.Send(query);
    }
}
