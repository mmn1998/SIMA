using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.TrustyDrafts.InquiryRequests;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.WebApi.Extensions;

namespace SIMA.WebApi.Controllers.Features.TrustyDrafts.InquiryRequests;

[Route("trusty/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Trusty/InquiryRequests")]
[Authorize]
public class InquiryRequestQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public InquiryRequestQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.InquiryRequestGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetInquiryRequestQuery { Id = id };
        var result = await _mediator.Send(query);

        if (result.Data.InquiryRquestDocumentList is not null)
        {
            foreach (var document in result.Data.InquiryRquestDocumentList)
            {
                document.DocumentContentType = document.DocumentExtensionName?.GetContentType();
            }
        }
        return result;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.InquiryRequestGetAll)]
    public async Task<Result> Get([FromBody] GetAllInquiryRequestsQuery query)
    {
        return await _mediator.Send(query);
    }
}
