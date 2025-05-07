using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.Resources;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.Resources.V1;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/Resources")]
public class ResourcesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourcesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.ResourcesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetResourceQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ResourcesGetAll)]
    public async Task<Result> Get([FromBody] GetAllResourcesQuery query)
    {
        return await _mediator.Send(query);
    }
}