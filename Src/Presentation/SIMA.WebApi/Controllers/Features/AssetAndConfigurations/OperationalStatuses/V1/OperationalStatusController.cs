using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.LicenseTypes;
using SIMA.Application.Contract.Features.AssetAndConfigurations.OperationalStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

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
    [SimaAuthorize(Permissions.OperationalStatusPost)]
    public async Task<Result> Post([FromBody] CreateOperationalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.OperationalStatusPut)]
    public async Task<Result> Put([FromBody] ModifyOperationalStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.OperationalStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteOperationalStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}