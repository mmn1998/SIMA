using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Locations;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Loactions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Locations")]
[Authorize]

public class LocationsController : ControllerBase
{

    private readonly IMediator _mediator;
    public LocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.LocationPost)]
    public async Task<Result> Post(CreateLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.LocationPut)]
    public async Task<Result> Put(ModifyLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.LocationDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeleteLocationCommand { Id = id };
        return await _mediator.Send(command);
    }



}
