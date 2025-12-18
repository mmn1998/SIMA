using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Departments.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Departments")]
[Authorize]

public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DepartmentsPost)]
    public async Task<Result> Post([FromBody] CreateDepartmentCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpPut]
    [SimaAuthorize(Permissions.DepartmentsPut)]
    public async Task<Result> Put([FromBody] ModifyDepartmentCommand command)
    {
        var result = await _mediator.Send(command);
        return result;
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DepartmentsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDepartmentCommand { Id = id };
        return await _mediator.Send(command);
    }
}
