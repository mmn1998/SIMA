using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.ConsequenceCategories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ConsequenceCategories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/ConsequenceCategories")]
public class ConsequenceCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.ConsequenceCategoryPost)]
    public async Task<Result> Post([FromBody] CreateConsequenceCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.ConsequenceCategoryPut)]
    public async Task<Result> Put([FromBody] ModifyConsequenceCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.ConsequenceCategoryDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteConsequenceCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}