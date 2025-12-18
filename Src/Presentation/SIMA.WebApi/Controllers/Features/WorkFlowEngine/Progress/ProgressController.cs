using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Application.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.Progress
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Progress")]
    [Authorize]
    public class ProgressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProgressController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPut]
        [SimaAuthorize(Permissions.ProgressPut)]
        public async Task<Result> ChangeStatus([FromBody] ModifyProgressCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
