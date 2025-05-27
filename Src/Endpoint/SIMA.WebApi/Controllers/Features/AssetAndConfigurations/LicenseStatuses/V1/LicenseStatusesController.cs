using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.LicenseStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/LicenseStatuses")]
public class LicenseStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LicenseStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.LicenseStatusPost)]
    public async Task<Result> Post([FromBody] CreateLicenseStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    //[SimaAuthorize(Permissions.LicenseStatusPut)]
    public async Task<Result> Put([FromBody] ModifyLicenseStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.LicenseStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteLicenseStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}