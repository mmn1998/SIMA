using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.UIInputElements;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.UIInputElements.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "UIInputElements")]
[Authorize]
public class UIInputElementsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UIInputElementsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.UIInputElementPost)]
    public async Task<Result> Post([FromBody] CreateUIInputElementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.UIInputElementPut)]
    public async Task<Result> Put([FromBody] ModifyUIInputElementCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.UIInputElementDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteUIInputElementCommand { Id = id };
        return await _mediator.Send(command);
    }
}