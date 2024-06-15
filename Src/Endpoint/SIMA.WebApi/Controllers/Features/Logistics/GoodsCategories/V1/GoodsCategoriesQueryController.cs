using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.GoodsCategories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsCategories")]
public class GoodsCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetGoodsCategoryQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllGoodsCategoriesQuery query)
    {
        return await _mediator.Send(query);
    }
}