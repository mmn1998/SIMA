using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Staffs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Staffs.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Staffs")]
[Authorize]

public class StaffsController : ControllerBase
{
    private readonly IMediator _mediator;
    public StaffsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [SimaAuthorize(Permissions.StaffsPost)]
    public async Task<Result> Post([FromBody] CreateStaffCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [SimaAuthorize(Permissions.StaffsPut)]
    public async Task<Result> Put([FromBody] ModifyStaffCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.StaffsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteStaffCommand { Id = id };
        return await _mediator.Send(command);
    }
}
