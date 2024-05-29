using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.IssueManagement.IssueWeightCategories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueWeightCategories.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "IssueWeightCategories")]
public class IssueWeightCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueWeightCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [SimaAuthorize(Permissions.IssueWeightCategoriesGetAll)]
    public async Task<Result> Get([FromQuery] GetAllIssueWeightCategoriesQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.IssueWeightCategoriesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetIssueWeightCategoryQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpGet("GetByWeight")]
    public async Task<Result> GetByWeight(int weight)
    {
        var query = new GetIssueWeightCategoryByWeightQuery { Weight = weight };
        return await _mediator.Send(query);
    }
}
