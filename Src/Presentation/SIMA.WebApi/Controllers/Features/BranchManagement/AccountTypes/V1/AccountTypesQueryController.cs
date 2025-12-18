using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.AccountTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.AccountTypes.V1;

[Route("branch/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Branch/AccountTypes")]
public class AccountTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetAccountTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllAccountTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}