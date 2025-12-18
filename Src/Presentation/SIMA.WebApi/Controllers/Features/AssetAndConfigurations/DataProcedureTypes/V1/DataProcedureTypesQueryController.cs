using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedureTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.DataProcedureTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/DataProcedureTypes")]
[Authorize]
public class DataProcedureTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DataProcedureTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.DataProcedureTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDataProcedureTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DataProcedureTypeGetAll)]
    public async Task<Result> Get([FromBody] GetAllDataProcedureTypesQuery query)
    {
        return await _mediator.Send(query);
    }


}