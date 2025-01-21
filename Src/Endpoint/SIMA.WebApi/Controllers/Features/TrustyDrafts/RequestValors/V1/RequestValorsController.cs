using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.RequestValors;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.RequestValors.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/RequestValors")]
public class RequestValorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestValorsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.RequestValorsPost)]
    public async Task<Result> Post([FromBody] CreateRequestValorCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.RequestValorsPut)]
    public async Task<Result> Put([FromBody] ModifyRequestValorCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.RequestValorsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteRequestValorCommand { Id = id };
        return await _mediator.Send(command);
    }
}