using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.BusinessCriticalities;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Categories;
using SIMA.Framework.Common.Response;

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
    public async Task<Result> Post([FromBody] CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCategoryCommand() { Id = id };
        return await _mediator.Send(command);
    }
}