using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlows.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkFlows")]
    [Authorize]

    public class WorkFlowsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkFlowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region -- Workflow --
        [HttpPost]
        [SimaAuthorize(Permissions.WorkFlowPost)]
        public async Task<Result> Post([FromBody] CreateWorkFlowCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [SimaAuthorize(Permissions.WorkFlowPut)]
        public async Task<Result> Put([FromBody] ModifyWorkFlowCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.WorkFlowDelete)]
        public async Task<Result> Delete(long id)
        {
            var command = new DeleteWorkFlowCommand { Id = id };
            return await _mediator.Send(command);
        }
        #endregion

        #region -- Step --
        [HttpPost("InsertStep")]
        [SimaAuthorize(Permissions.WorkFlowStepPost)]
        public async Task<Result> Post([FromBody] CreateStepCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("ModifyStep")]
        [SimaAuthorize(Permissions.WorkFlowStepPut)]
        public async Task<Result> Put([FromBody] ModifyStepCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteStep/{id}/{workFlowId}")]
        [SimaAuthorize(Permissions.WorkFlowStepDelete)]
        public async Task<Result> Delete(long id, long workFlowId)
        {
            var command = new DeleteStepCommand { Id = id, WorkFlowId = workFlowId };
            return await _mediator.Send(command);
        }
        #endregion

        #region -- State --
        [HttpPost("InsertState")]
        [SimaAuthorize(Permissions.WorkFlowStatePost)]
        public async Task<Result> Post([FromBody] CreateStateCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("ModifyState")]
        [SimaAuthorize(Permissions.WorkFlowStatePut)]
        public async Task<Result> Put([FromBody] ModifyStateCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteState/{id}/{workFlowId}")]
        [SimaAuthorize(Permissions.WorkFlowStateDelete)]
        public async Task<Result> DeleteState(long id, long workFlowId)
        {
            var command = new DeleteStateCommand { Id = id, WorkFlowId = workFlowId };
            return await _mediator.Send(command);
        }
        #endregion

    }
}
