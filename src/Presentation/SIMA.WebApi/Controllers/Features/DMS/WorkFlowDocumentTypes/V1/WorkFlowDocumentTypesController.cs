using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.WorkFlowDocumentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkflowDocumentTypes")]
public class WorkflowDocumentTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowDocumentTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.WorkFlowDocumentTypesPost)]
    public async Task<Result> Post([FromBody] CreateWorkflowDocumentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.WorkFlowDocumentTypesPut)]
    public async Task<Result> Put([FromBody] ModifyWorkflowDocumentTypeCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.WorkFlowDocumentTypesDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteWorkflowDocumentTypeCommand { Id = id };
        return await _mediator.Send(command);
    }
}