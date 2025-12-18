using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.CobitCategories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.CobitCategories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/CobitCategories")]
public class CobitCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CobitCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.CobitCategoryGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCobitCategoryQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.CobitCategoryGetAll)]
    public async Task<Result> Get([FromBody] GetAllCobitCategoriesQuery query)
    {
        return await _mediator.Send(query);
    }
}