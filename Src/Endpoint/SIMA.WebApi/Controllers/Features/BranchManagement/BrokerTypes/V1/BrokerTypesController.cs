using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.BrokerTypes.V1;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/BrokerTypes")]
[Authorize]
public class BrokerTypesController : ControllerBase
{
    private readonly IMediator _mediator;
    public BrokerTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.BrokerTypePost)]
    public async Task<Result> Post([FromBody] CreateBrokerTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.BrokerTypePut)]
    public async Task<Result> Put([FromBody] ModifyBrokerTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.BrokerTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBrokerTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
