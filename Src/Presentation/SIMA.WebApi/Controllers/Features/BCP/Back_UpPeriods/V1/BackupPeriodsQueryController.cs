using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.Back_UpPeriods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BCP.Back_UpPeriods.V1;

[Route("bcp/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "BCP/BackupPeriods")]
public class BackupPeriodsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackupPeriodsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBackupPeriodQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllBackupPeriodsQuery query)
    {
        return await _mediator.Send(query);
    }
}