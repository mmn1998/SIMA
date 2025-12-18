using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.Back_UpPeriods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.Back_UpPeriods.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BackupPeriods")]
public class BackupPeriodsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackupPeriodsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateBackupPeriodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyBackupPeriodCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBackupPeriodCommand { Id = id };
        return await _mediator.Send(command);
    }
}