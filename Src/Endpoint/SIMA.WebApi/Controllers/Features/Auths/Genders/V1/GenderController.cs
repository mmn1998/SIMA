using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Genders;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Genders.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Genders")]
[Authorize]

public class GendersController : ControllerBase
{
    private readonly IMediator _mediator;
    public GendersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.GenderPost)]
    public async Task<Result> Post(CreateGenderCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.GenderPut)]
    public async Task<Result> Put(ModifyGenderCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.GenderDelete)]
    public async Task<Result<long>> Delete(long id)
    {
        var command = new DeleteGenderCommand { Id = id };
        return await _mediator.Send(command);
    }
}
