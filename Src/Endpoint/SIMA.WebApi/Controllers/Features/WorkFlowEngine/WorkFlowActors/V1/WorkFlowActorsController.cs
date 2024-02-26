using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlowActors.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkFlowActors")]
    [Authorize]

    public class WorkFlowActorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkFlowActorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.WorkFlowActorPost)]
        public async Task<Result> Post([FromBody] CreateWorkFlowActorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [SimaAuthorize(Permissions.WorkFlowActorPut)]
        public async Task<Result> Put([FromBody] ModifyWorkFlowActorCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }


        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.WorkFlowActorDelete)]
        public async Task<Result> Delete(long id)
        {
            var command = new DeleteWorkFlowActorCommand { Id = id };
            return await _mediator.Send(command);
        }

        [HttpPost("CreateRole")]
        [SimaAuthorize(Permissions.WorkFlowActorRolePost)]
        public async Task<Result> RolePost([FromBody] CreateWorkFlowActorRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteRole/{id}/{workFlowActorId}")]
        [SimaAuthorize(Permissions.WorkFlowActorRoleDelete)]
        public async Task<Result> RoleDelete(long id, long workFlowActorId)
        {
            var command = new DeleteWorkFlowActorRoleCommand { Id = id, WorkFlowActorId = workFlowActorId };
            return await _mediator.Send(command);
        }

        [HttpPost("CreateUser")]
        [SimaAuthorize(Permissions.WorkFlowActorUserPost)]
        public async Task<Result> UserPost([FromBody] CreateWorkFlowActorUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteUser/{id}/{workFlowActorId}")]
        [SimaAuthorize(Permissions.WorkFlowActorUserDelete)]
        public async Task<Result> UserDelete(long id, long workFlowActorId)
        {
            var command = new DeleteWorkFlowActorUserCommand { Id = id, WorkFlowActorId = workFlowActorId };
            return await _mediator.Send(command);
        }

        [HttpPost("CreateGroup")]
        [SimaAuthorize(Permissions.WorkFlowActorGroupPost)]
        public async Task<Result> GroupPost([FromBody] CreateWorkFlowActorGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("DeleteGroup/{id}/{workFlowActorId}")]
        [SimaAuthorize(Permissions.WorkFlowActorGroupDelete)]
        public async Task<Result> GroupDelete(long id, long workFlowActorId)
        {
            var command = new DeleteWorkFlowActorGroupCommand { Id = id, WorkFlowActorId = workFlowActorId };
            return await _mediator.Send(command);
        }
    }
}
