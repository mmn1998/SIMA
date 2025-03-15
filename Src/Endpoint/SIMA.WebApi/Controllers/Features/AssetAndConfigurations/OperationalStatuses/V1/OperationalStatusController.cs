using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;
using SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.OperationalStatuses.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/OperationalStatus")]
[Authorize]
public class OperationalStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public OperationalStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    // [SimaAuthorize(Permissions.LicenseTypePost)]
    public async Task<Result> Post([FromBody] CreateOperationalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    // [SimaAuthorize(Permissions.LicenseTypePut)]
    public async Task<Result> Put([FromBody] ModifyOperationalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.LicenseTypeDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteOperationalStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}