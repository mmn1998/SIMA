using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.DMS.Documents.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Documents")]
public class DocumentsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download([FromRoute] long id)
    {
        var query = new GetDownloadDocumentQuery { DocumetId = id };
        var response = await _mediator.Send(query);
        var contentType = response.Extension.GetContentType();
        return File(response.FileContent, contentType);
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDownloadDocumentQuery { DocumetId = id };
        var response = await _mediator.Send(query);
        response.ContentType = response.Extension.GetContentType();
        return Result.Ok(response);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get(GetAllDocumentsQuery query)
    {
        return await _mediator.Send(query);
    }
}
