using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.AssetAndConfigurations.Back_UpMethods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.AssetAndConfigurations.Back_UpMethods.V1;

[Route("assetAndConfiguration/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Asset/BackupMethods")]
[Authorize]
public class BackupMethodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackupMethodsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    // [SimaAuthorize(Permissions.BackupMethodPost)]
    public async Task<Result> Post([FromBody] CreateBackupMethodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    // [SimaAuthorize(Permissions.BackupMethodPut)]
    public async Task<Result> Put([FromBody] ModifyBackupMethodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    //[SimaAuthorize(Permissions.BackupMethodDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBackupMethodCommand { Id = id };
        return await _mediator.Send(command);
    }
}