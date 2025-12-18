using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.Categories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Categories.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Category")]
public class CategoryQueryController: ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.CategoryGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCategoryQuery() { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.CategoryGetAll)]
    public async Task<Result> Get([FromBody] GetAllCategoryQuery query)
    {
        return await _mediator.Send(query);
    }
}