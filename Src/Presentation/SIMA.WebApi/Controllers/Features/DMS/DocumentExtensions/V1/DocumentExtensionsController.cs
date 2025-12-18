using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.DMS.DocumentExtensions;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.DocumentExtensions.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "DocumentExtensions")]
public class DocumentExtensionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentExtensionsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DocumentExtensionsPost)]
    public async Task<Result> Post([FromBody] CreateDocumentExtensionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DocumentExtensionsPut)]
    public async Task<Result> Put([FromBody] ModifyDocumentExtensionCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DocumentExtensionsDelete)]
    public async Task<Result> Delete(long id)
    {
        var command = new DeleteDocumentExtensionCommand { Id = id };
        return await _mediator.Send(command);
    }
}
