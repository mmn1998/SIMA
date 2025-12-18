using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.Brokers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.Brokers.V1;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/Brokers")]
[Authorize]
public class BrokersController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateBrokerCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyBrokerCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBrokerCommand { Id = id };
        return await _mediator.Send(command);
    }
}
