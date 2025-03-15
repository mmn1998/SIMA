using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataProcedures.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataProcedures")]
[Authorize]
public class DataProceduresQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataProceduresQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDataProcedureQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromBody] GetAllDataProceduresQuery query)
    {
        return await _mediator.Send(query);
    }


}
