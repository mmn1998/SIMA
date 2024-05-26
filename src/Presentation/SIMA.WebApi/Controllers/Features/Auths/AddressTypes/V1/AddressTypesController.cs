using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.AddressTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.AddressTypes.V1;
[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "AddressTypes")]
[Authorize]

public class AddressTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.AddressTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteAddressTypeCommand { Id = id };
        return await _mediator.Send(command);
    }

    [HttpPost]
    [SimaAuthorize(Permissions.AddressTypesPost)]
    public async Task<Result> Post([FromBody] CreateAddressTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.AddressTypesPut)]
    public async Task<Result> Put(ModifyAddressTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }
}
