using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.LicenseTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/LicenseTypes")]
[Authorize]
public class LicenseTypeQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseTypeQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetLicenseTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
   //[SimaAuthorize(Permissions.)]
    public async Task<Result> Get([FromBody] GetAllLicenseTypeQuery query)
    {
        return await _mediator.Send(query);
    }

   
}