using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryResponses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.InquiryResponses;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/InquiryResponses")]
[Authorize]
public class InquiryResponseQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InquiryResponseQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.InquiryResponseGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetInquiryResponseQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.InquiryResponseGetAll)]
    public async Task<Result> Get([FromBody] GetAllInquiryResponsesQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("GetAllAcceptInqueryResponse")]
    [SimaAuthorize(Permissions.InquiryResponseGetAll)]
    public async Task<Result> GetAllAcceptInqueryResponse()
    {
        var query = new GetAllAcceptInqueryResponseQuery();
        return await _mediator.Send(query);
    }
}
