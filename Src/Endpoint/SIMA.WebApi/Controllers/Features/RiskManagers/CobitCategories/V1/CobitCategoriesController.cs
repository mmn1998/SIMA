using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.CobitCategories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CobitCategories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CobitCategories")]
public class CobitCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CobitCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.CobitCategoryPost)]
    public async Task<Result> Post([FromBody] CreateCobitCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.CobitCategoryPut)]
    public async Task<Result> Put([FromBody] ModifyCobitCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.CobitCategoryDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteCobitCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}