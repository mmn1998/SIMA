using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.DMS.WorkFlowDocumentExtensions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.DMS.WorkFlowDocumentExtentions.V1
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkFlowDocumentExtentions")]
    public class WorkFlowDocumentExtentionsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WorkFlowDocumentExtentionsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result> Get([FromQuery] BaseRequest request)
        {
            var command = new GetAllWorkFlowDocumentExtensionQuery { Request = request };
            return await _mediator.Send(command);
        }

        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var command = new GetWorkFlowDocumentExtensionQuery { Id = id };
            return await _mediator.Send(command);
        }
    }
}
