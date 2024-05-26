using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.Branches;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.Branches.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branches")]
[Authorize]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.BranchPost)]
    public async Task<Result> Post([FromBody] CreateBranchCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.BranchPut)]
    public async Task<Result> Put([FromBody] ModifyBranchCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.BranchDelete)]

    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBranchCommand { Id = id };
        return await _mediator.Send(command);
    }
}
