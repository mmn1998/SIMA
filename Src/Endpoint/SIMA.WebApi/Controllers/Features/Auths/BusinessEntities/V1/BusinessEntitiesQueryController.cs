using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.BusinessEntities;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.BusinessEntities.V1;

[Route("basic/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BusinessEntities")]
[Authorize]
public class BusinessEntitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessEntitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllBusinessEntitiesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBusinessEntityQuery { Id = id };
        return await _mediator.Send(query);
    }
}