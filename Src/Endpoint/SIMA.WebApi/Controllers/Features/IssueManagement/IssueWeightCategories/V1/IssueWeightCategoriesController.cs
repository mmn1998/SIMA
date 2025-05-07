using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueWeightCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssueWeightCategories")]
public class IssueWeightCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueWeightCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.IssueWeightCategoriesPost)]
    public async Task<Result> Post([FromBody] CreateIssueWeightCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.IssueWeightCategoriesPut)]
    public async Task<Result> Put([FromBody] ModifyIssueWeightCategoryCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.IssueWeightCategoriesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteIssueWeightCategoryCommand { Id = id };
        return await _mediator.Send(command);
    }
}
