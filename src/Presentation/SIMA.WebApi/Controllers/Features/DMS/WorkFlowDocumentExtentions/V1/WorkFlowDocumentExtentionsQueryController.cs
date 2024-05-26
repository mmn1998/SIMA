using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.DMS.WorkFlowDocumentExtentions.V1
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkflowDocumentExtentions")]
    public class WorkflowDocumentExtentionsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WorkflowDocumentExtentionsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result> Get([FromQuery] GetAllWorkFlowDocumentExtensionQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetWorkFlowDocumentExtensionQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
