using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Staffs;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Staffs.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Staffs")]
[Authorize]

public class StaffsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public StaffsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.StaffsGetAll)]
    public async Task<Result> Get(GetAllStaffQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.StaffsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetStaffQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }
    [HttpGet("StaffByDepartment/{departmentId}")]
    [SimaAuthorize(Permissions.StaffsGet)]
    public async Task<Result> GetStaffByDepartment([FromRoute] long? departmentId)
    {
        var query = new GetStaffByDepartmentQuery { DepartmentId = departmentId };
        var result = await _mediator.Send(query);
        return result;
    }
    [HttpGet("GetByStaffNumber/{staffNumber}")]
    public async Task<Result> GetByStaffNumber([FromRoute] string staffNumber)
    {
        var query = new GetStaffByStaffNumberQuery { StaffNumber = staffNumber };
        return await _mediator.Send(query);
    }
    [HttpGet("GetByBranch/{staffNumber}")]
    public async Task<Result> GetByBranchId([FromRoute] long branchId)
    {
        var query = new GetStaffByBranchQuery { BranchId = branchId };
        return await _mediator.Send(query);
    }
}
