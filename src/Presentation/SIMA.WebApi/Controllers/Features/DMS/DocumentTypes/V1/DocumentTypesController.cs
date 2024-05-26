using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.DMS.DocumentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.DocumentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "DocumentTypes")]
public class DocumentTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.DocumentTypesPost)]
    public async Task<Result> Post([FromBody] CreateDocumentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.DocumentTypesPut)]
    public async Task<Result> Put([FromBody] ModifyDocumentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.DocumentTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteDocumentTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}
