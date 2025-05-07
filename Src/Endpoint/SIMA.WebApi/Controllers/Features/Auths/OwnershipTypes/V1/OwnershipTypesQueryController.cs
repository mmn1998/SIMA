using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.OwnershipTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "OwnershipTypes")]
[Authorize]
public class OwnershipTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public OwnershipTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllOwnershipTypesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetOwnershipTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}