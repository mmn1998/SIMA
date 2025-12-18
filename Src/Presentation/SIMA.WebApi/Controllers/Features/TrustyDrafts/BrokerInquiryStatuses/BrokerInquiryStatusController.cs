using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.BrokerInquiryStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.BrokerInquiryStatuses;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/BrokerInquiryStatuses")]
[Authorize]
public class BrokerInquiryStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrokerInquiryStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.brokerInquiryStatusPost)]
    public async Task<Result> Post([FromBody] CreateBrokerInquiryStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.brokerInquiryStatusPut)]
    public async Task<Result> Put([FromBody] ModifyBrokerInquiryStatusCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.brokerInquiryStatusDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteBrokerInquiryStatusCommand { Id = id };
        return await _mediator.Send(command);
    }
}