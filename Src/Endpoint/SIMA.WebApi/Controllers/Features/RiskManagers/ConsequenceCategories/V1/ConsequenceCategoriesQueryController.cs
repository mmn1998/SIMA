using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.ConsequenceCategories;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ConsequenceCategories.V1;

[Route("RiskManagement/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "RiskManagement/ConsequenceCategories")]
public class ConsequenceCategoriesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConsequenceCategoriesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.ConsequenceCategoryGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetConsequenceCategoryQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.ConsequenceCategoryGetAll)]
    public async Task<Result> Get([FromBody] GetAllConsequenceCategoriesQuery query)
    {
        return await _mediator.Send(query);
    }
}