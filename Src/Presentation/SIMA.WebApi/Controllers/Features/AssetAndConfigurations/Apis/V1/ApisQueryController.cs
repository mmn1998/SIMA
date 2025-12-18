using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.DataProcedures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Apis.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/Apis")]
[Authorize]
public class ApisQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public ApisQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetApiQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromBody] GetAllApisQuery query)
    {
        return await _mediator.Send(query);
    }


}
