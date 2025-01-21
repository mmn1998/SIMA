using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.LogisticRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.LogisticsRequest;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LogisticsRequests")]
[Authorize]
public class LogisticsRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogisticsRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.LogisticsRequestsPost)]
    public async Task<Result> Post([FromBody] CreateLogisticRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [SimaAuthorize(Permissions.LogisticsRequestsPut)]
    public async Task<Result> Put([FromBody] ModifyLogisticsRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.LogisticsRequestsDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteLogisticRequestCommand { Id = id };
        return await _mediator.Send(command);
    }
}