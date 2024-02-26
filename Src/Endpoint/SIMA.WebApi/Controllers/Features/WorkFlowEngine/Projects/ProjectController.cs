using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.Projects
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Projects")]
    [Authorize]
    public class ProjectController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [SimaAuthorize(Permissions.ProjectsPost)]
        public async Task<Result> Post([FromBody] CreateProjectCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost("CreateProjectGroup")]
        [SimaAuthorize(Permissions.ProjectGroupsPost)]
        public async Task<Result> Post([FromBody] CreateProjectGroupCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("CreateProjectMember")]
        [SimaAuthorize(Permissions.ProjectMembersPost)]
        public async Task<Result> Post([FromBody] CreateProjectMemberCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [SimaAuthorize(Permissions.ProjectsPut)]
        public async Task<Result> Put([FromBody] ModifyProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.ProjectsDelete)]
        public async Task<Result> Delete(long id)
        {
            var command = new DeleteProjectCommand { Id = id };
            return await _mediator.Send(command);
        }

        [HttpDelete("ProjectGroup/{id}/{projectId}")]
        [SimaAuthorize(Permissions.ProjectGroupsDelete)]
        public async Task<Result> Delete(int id, int projectId)
        {
            var args = new DeleteProjectGroupCommand { Id = id, ProjectId = projectId };
            var result = await _mediator.Send(args);
            return result;
        }

        [HttpDelete("ProjectMember/{id}/{projectId}")]
        [SimaAuthorize(Permissions.ProjectMemberDelete)]
        public async Task<Result> DeleteProjectMember(int id, int projectId)
        {
            var args = new DeleteProjectGroupCommand { Id = id, ProjectId = projectId };
            var result = await _mediator.Send(args);
            return result;
        }
    }
}
