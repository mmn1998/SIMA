using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsTypes")]
[Authorize]
public class GoodsTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GoodsTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GoodsTypesGetAll)]
    public async Task<Result> Get([FromBody] GetAllGoodsTypeQuery query)
    {
        return await _mediator.Send(query);
    }
}