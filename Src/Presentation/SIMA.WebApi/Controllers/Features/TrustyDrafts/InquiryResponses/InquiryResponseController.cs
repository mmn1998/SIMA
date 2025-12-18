using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.InquiryResponses;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/InquiryResponses")]
[Authorize]

public class InquiryResponseController : ControllerBase
{
    private readonly IMediator _mediator;

    public InquiryResponseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.InquiryResponsePost)]
    public async Task<Result> Post([FromBody] CreateInquiryResponseCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.InquiryResponsePut)]
    public async Task<Result> Put([FromBody] ModifyInquiryResponseCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.InquiryResponseDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteInquiryResponseCommand { Id = id };
        return await _mediator.Send(command);
    }
}
