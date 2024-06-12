using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.ApprovalOptions;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.ApprovalOptions
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApprovalOptions")]
    [Authorize]
    public class ApprovalOptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApprovalOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateApprovalOptionCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyApprovalOptionCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteApprovalOptionCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
