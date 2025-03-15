using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.OperationalStatuses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.OperationalStatuses.V1;


[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/OperationalStatus")]
[Authorize]
public class OperationalStatusQueryController
{
     private readonly IMediator _mediator;

     public OperationalStatusQueryController(IMediator mediator)
     {
         _mediator = mediator;  
     }
     [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetOperationalStatusQuery() { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
   //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromBody] GetAllOperationalStatusQuery query)
    {
        return await _mediator.Send(query);
    }
}