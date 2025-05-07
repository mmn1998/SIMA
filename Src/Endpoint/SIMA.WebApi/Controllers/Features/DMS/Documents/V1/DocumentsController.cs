using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.DMS.Documents;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.Documents.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Documents")]
[Authorize]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateDocumentCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPost("multi")]
    // 6MB
    [RequestSizeLimit(6291456)]
    public async Task<Result> Post([FromBody] MultiCreateDocumentCommand command)
    {
        return await _mediator.Send(command);
    }
    //[HttpPut]
    //public async Task<Result> Put([FromBody] ModifyDocumentCommand command)
    //{
    //    return await _mediator.Send(command);
    //}
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDocumentCommand { Id = id };
        return await _mediator.Send(command);
    }
}
