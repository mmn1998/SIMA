using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Categories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Categories.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Category")]
public class CategoryController: ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.CategoryPost)]
    public async Task<Result> Post([FromBody] CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.CategoryPut)]
    public async Task<Result> Put([FromBody] ModifyCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.CategoryDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCategoryCommand() { Id = id };
        return await _mediator.Send(command);
    }
}