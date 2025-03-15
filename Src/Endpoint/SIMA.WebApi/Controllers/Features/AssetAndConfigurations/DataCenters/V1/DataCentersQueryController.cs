using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataCenters;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataCenters.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataCenters")]
[Authorize]
public class DataCentersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataCentersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDataCenterQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromBody] GetAllDataCentersQuery query)
    {
        return await _mediator.Send(query);
    }


}