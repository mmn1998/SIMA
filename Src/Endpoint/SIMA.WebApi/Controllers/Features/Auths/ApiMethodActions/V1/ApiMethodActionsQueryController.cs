using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.ApiMethodActions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.ApiMethodActions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "ApiMethodActions")]
[Authorize]
public class ApiMethodActionsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApiMethodActionsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllApiMethodActionsQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetApiMethodActionQuery { Id = id };
        return await _mediator.Send(query);
    }
}