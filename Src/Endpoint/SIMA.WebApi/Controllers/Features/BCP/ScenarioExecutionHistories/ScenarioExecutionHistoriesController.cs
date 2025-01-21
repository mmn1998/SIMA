using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.ScenarioExecutionHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.ScenarioExecutionHistories
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BCP/ScenarioExecutionHistories")]
    [Authorize]
    public class ScenarioExecutionHistoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScenarioExecutionHistoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.senarioExecutionHistoryPost)]
        public async Task<Result> Post([FromBody] CreateScenarioExecutionHistoryCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.senarioExecutionHistoryPut)]
        public async Task<Result> Put([FromBody] ModifyScenarioExecutionHistoryCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.senarioExecutionHistoryDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteScenarioExecutionHistoryCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
