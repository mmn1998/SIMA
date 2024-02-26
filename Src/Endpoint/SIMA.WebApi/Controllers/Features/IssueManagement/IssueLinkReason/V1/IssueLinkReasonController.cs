using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.IssueManagement.IssueLinkReasons;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.IssueManagement.IssueLinkReason.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "IssueLinkReasons")]
    public class IssueLinkReasonController : Controller
    {
        private readonly IMediator _mediator;
        public IssueLinkReasonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.IssueLinkReasonsDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteIssueLinkReasonCommand { Id = id };
            return await _mediator.Send(command);
        }

        [HttpPost]
        [SimaAuthorize(Permissions.IssueLinkReasonsPost)]
        public async Task<Result> Post([FromBody] CreateIssueLinkReasonCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [SimaAuthorize(Permissions.IssueLinkReasonsPut)]
        public async Task<Result> Put(ModifyIssueLinkReasonCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
