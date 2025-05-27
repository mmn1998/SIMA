using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.AssetsAndConfigurations.LicenseStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.LicenseStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/LicenseStatuses")]
public class LicenseStatusesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseStatusesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    //[SimaAuthorize(Permissions.LicenseStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetLicenseStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    //[SimaAuthorize(Permissions.LicenseStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllLicenseStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}