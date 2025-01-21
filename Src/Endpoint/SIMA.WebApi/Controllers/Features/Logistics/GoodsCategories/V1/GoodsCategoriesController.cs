using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.GoodsCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.GoodsCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "GoodsCategories")]
//[Authorize]
public class GoodsCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GoodsCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.GoodsCategoriesPost)]

    public async Task<Result> Post([FromBody] CreateGoodsCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.GoodsCategoriesPut)]
    public async Task<Result> Put([FromBody] ModifyGoodsCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.GoodsCategoriesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteGoodsCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}