using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.OrganizationalProjects.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "OrganizationalProjects")]
    public class OrganizationalProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationalProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        //[SimaAuthorize(Permissions.ServiceCategoryPost)]
        public async Task<Result> Post([FromBody] CreateOrganizationalProjectCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        //[SimaAuthorize(Permissions.ServiceCategoryPut)]
        public async Task<Result> Put([FromBody] ModifyOrganizationalProjectCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        //[SimaAuthorize(Permissions.ServiceCategoryDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteOrganizationalProjectCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
