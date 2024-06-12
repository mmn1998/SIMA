using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.ApprovalOptions
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApprovalOptions")]
    [Authorize]
    public class ApprovalOptionQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApprovalOptionQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get([FromBody] GetAllApprovalOptionsQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetApprovalOptionQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
