using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.LocationTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.LoactionTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LocationTypes")]
[Authorize]

public class LocationTypesController : ControllerBase
{

    private readonly IMediator _mediator;
    public LocationTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.LocationTypePost)]
    public async Task<Result> Post(CreateLocationTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.LocationTypePut)]
    public async Task<Result> Put(ModifyLocationTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.LocationTypeDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeleteLocationTypeCommand { Id = id };
        return await _mediator.Send(command);
    }



}
