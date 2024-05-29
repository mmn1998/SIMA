using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.Branches;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.Branches.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branches")]
[Authorize]
public class BranchesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    [SimaAuthorize(Permissions.BranchGetAll)]
    public async Task<Result> Get([FromQuery] GetAllBranchQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.BranchGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBranchQuery { Id = id };
        return await _mediator.Send(query);
    }
}
