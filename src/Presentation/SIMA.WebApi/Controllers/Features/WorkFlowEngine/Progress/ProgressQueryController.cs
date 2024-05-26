using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Framework.Common.Response;

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
        //[SimaAuthorize(Permissions.ProjectGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetProgressQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        //[SimaAuthorize(Permissions.ProjectGetAll)]
        public async Task<Result> Get([FromQuery] GetAllProgressQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
