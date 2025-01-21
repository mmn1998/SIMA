using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.BrokerInquiryStatuses;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/BrokerInquiryStatuses")]
[Authorize]
public class BrokerInquiryStatusQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokerInquiryStatusQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.brokerInquiryStatusGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetBrokerInquiryStatusQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.brokerInquiryStatusGetAll)]
    public async Task<Result> Get([FromBody] GetAllBrokerInquiryStatusesQuery query)
    {
        return await _mediator.Send(query);
    }
}