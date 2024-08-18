using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.ResponsibleTypes.V1;

[Route("basic/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ResponsibleTypes")]
[Authorize]

public class ResponsibleTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsibleTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    //[SimaAuthorize(Permissions.ResponsibleTypePost)]
    public async Task<Result> Post([FromBody] CreateResponsibleTypeCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    //  [SimaAuthorize(Permissions.ResponsibleTypePut)]
    public async Task<Result> Put([FromBody] ModifyResponsibleTypeCommands command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    // [SimaAuthorize(Permissions.ResponsibleTypeDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeleteResponsibleTypeCommand { Id = id };
        return await _mediator.Send(command);
    }


}
