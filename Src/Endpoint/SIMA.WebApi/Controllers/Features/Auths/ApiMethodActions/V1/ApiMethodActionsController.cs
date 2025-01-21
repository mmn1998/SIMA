using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.ApiMethodActions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.ApiMethodActions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ApiMethodActions")]
[Authorize]
public class ApiMethodActionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiMethodActionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.apiActionMethodPost)]
    public async Task<Result> Post([FromBody] CreateApiMethodActionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.apiActionMethodPut)]
    public async Task<Result> Post([FromBody] ModifyApiMethodActionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.apiActionMethodDelete)]
    public async Task<Result> Post([FromRoute] long id)
    {
        var command = new DeleteApiMethodActionCommand { Id = id };
        return await _mediator.Send(command);
    }
}