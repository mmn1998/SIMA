using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.LicenseTypes.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/LicenseTypes")]
[Authorize]
public class LicenseTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
   // [SimaAuthorize(Permissions.LicenseTypePost)]
    public async Task<Result> Post([FromBody] CreateLicenseTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
   // [SimaAuthorize(Permissions.LicenseTypePut)]
    public async Task<Result> Put([FromBody] ModifyLicenseTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.LicenseTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteLicenseTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
