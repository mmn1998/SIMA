using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.Projects
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Projects")]
    [Authorize]

    public class ProjectQueryController : Controller
    {
        private readonly IMediator _mediator;
        public ProjectQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.ProjectGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetProjectQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.ProjectGetAll)]
        public async Task<Result> Get(GetAllProjectsQuery query)
        {
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetProjectByDomain/{domainId}")]
        [SimaAuthorize(Permissions.ProjectGetAll)]
        public async Task<Result> GetByDomain(long domainId)
        {
            var query = new GetProjectsByDomainQuery { DomainId = domainId};
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
