using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.DMS.WorkFlowDocumentExtentions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.DMS.WorkFlowDocumentExtentions.V1;


[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkflowDocumentExtentions")]
public class WorkflowDocumentExtentionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public WorkflowDocumentExtentionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Result> Post([FromBody] CreateWorkFlowDocumentExtentionCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    public async Task<Result> Put([FromBody] ModifyWorkFlowDocumentExtentionCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteWorkFlowDocumentExtentionCommand { Id = id };
        return await _mediator.Send(command);
    }
}
