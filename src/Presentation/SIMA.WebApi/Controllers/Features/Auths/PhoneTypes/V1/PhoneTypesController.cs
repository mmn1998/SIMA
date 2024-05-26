using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.PhoneTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.PhoneTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "PhoneTypes")]
[Authorize]

public class PhoneTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PhoneTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.PhoneTypesPut)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeletePhoneTypeCommand { Id = id };
        return await _mediator.Send(command);
    }


    [HttpPost]
    [SimaAuthorize(Permissions.PhoneTypesPost)]
    public async Task<Result> Post(CreatePhoneTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.PhoneTypesPut)]
    public async Task<Result> Put(ModifyPhoneTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;

    }
}
