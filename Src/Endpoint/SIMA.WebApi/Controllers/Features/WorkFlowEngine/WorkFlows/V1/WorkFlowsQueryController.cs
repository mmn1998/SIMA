using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlows.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkFlows")]
    [Authorize]

    public class WorkFlowsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkFlowsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region -- Workflow --
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.WorkFlowGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetWorkFlowQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        [SimaAuthorize(Permissions.WorkFlowGetAll)]
        public async Task<Result> Get()
        {
            var query = new GetAllWorkFlowsQuery();
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("GetWorkFlowByProject/{projectId}")]
        [SimaAuthorize(Permissions.WorkFlowGet)]
        public async Task<Result> GetByProjectId(long projectId)
        {
            var query = new GetWorkFlowByProjectQuery { ProjectId = projectId };
            var result = await _mediator.Send(query);
            return result;
        }
        #endregion

        #region -- Step --
        [HttpGet("GetSteps")]
        [SimaAuthorize(Permissions.WorkFlowStepGetAll)]
        public async Task<Result> GetSteps([FromQuery] GetAllStepsQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("GetStepById/{id}")]
        [SimaAuthorize(Permissions.WorkFlowStepGet)]
        public async Task<Result> GetSteps(long id)
        {
            var query = new GetStepQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("GetStepsByWorkFlowId/{id}")]
        [SimaAuthorize(Permissions.WorkFlowStepGet)]
        public async Task<Result> GetStepsByWorkFlowId(long id)
        {
            var query = new GetStepsByWorkFlowQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }
        #endregion

        #region -- State --
        [HttpGet("GetStates")]
        [SimaAuthorize(Permissions.WorkFlowStateGetAll)]
        public async Task<Result> GetStates([FromQuery] GetAllStatesQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("GetStateById/{id}")]
        [SimaAuthorize(Permissions.WorkFlowStateGet)]
        public async Task<Result> GetStates(long id)
        {
            var query = new GetStateQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }
        
        [HttpGet("GetStatesByWorkFlow/{id}")]
        [SimaAuthorize(Permissions.WorkFlowStateGet)]
        public async Task<Result> GetStatesByWorkFlow(long id)
        {
            var query = new GetStatesByWorkFlowQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }
        #endregion

    }
}
