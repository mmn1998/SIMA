using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.Progress
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Progress")]
    [Authorize]
    public class ProgressQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProgressQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.ProgressGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetProgressQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.ProgressGetAll)]
        public async Task<Result> Get(GetAllProgressQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
