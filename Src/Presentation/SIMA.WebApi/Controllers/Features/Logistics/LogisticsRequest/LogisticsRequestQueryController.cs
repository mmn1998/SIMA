using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.LogisticsRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.LogisticsRequest;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "LogisticsRequests")]
[Authorize]

public class LogisticsRequestQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LogisticsRequestQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.LogisticsRequestsGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetLogisticRequestsQuery { Id = id };
        return await _mediator.Send(query);
    }

    [HttpPost("GetGoodsForCoding/{Id}")]
    public async Task<Result> GetLogesticRequestGoods([FromRoute] long Id ,  [FromBody] GetLogesticRequestGoodsQuery query)
    {
        var result = new GetLogesticRequestGoodsQuery { Id = Id };
        return await _mediator.Send(result);
    }

    [HttpGet("GetLogisticsRequestGoods/{logisticsRequestId}")]
    public async Task<Result> GetLogisticsRequestGoodsFiltered([FromRoute] long logisticsRequestId)
    {
        var query = new GetLogisticsRequestGoodsQuery { LogisticsRequestId = logisticsRequestId };
        return await _mediator.Send(query);
    }
    [HttpGet("GetLogisticsRequestCodes")]
    public async Task<Result> GetLogisticsRequestCode()
    {
        var query = new GetLogisticsRequestCodeQuery();
        return await _mediator.Send(query);
    }


}

