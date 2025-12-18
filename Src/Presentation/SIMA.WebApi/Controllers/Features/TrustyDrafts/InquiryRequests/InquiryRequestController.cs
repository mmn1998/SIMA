using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.InquiryRequests;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/InquiryRequests")]
[Authorize]
public class InquiryRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public InquiryRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    //[SimaAuthorize(Permissions.InquiryRequestPost)]
    public async Task<Result> Post([FromBody] CreateInquiryRequestCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.InquiryRequestPut)]
    public async Task<Result> Put([FromBody] ModifyInquiryRequestCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.InquiryRequestDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteInquiryRequestCommand { Id = id };
        return await _mediator.Send(command);
    }
}
