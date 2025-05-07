using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.BusinessCriticalities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.BusinessCriticalities.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/BusinessCriticalities")]
public class BusinessCriticalitiesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BusinessCriticalitiesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.BusinessCriticalityGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBusinessCriticalityQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.BusinessCriticalityGetAll)]
    public async Task<Result> Get([FromBody] GetAllBusinessCriticalitiesQuery query)
    {
        return await _mediator.Send(query);
    }
}