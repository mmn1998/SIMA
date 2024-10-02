using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.DMS.WorkFlowDocumentTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "WorkflowDocumentTypes")]
public class WorkflowDocumentTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowDocumentTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.WorkFlowDocumentTypesGetAll)]
    public async Task<Result> Get(GetAllWorkFlowDocumentTypesQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.WorkFlowDocumentTypesGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetWorkFlowDocumentTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
}
