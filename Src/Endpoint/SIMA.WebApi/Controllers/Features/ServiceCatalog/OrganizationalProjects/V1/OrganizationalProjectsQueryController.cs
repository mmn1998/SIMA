using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.OrganizationalProjects;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.OrganizationalProjects.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "OrganizationalProjects")]
    public class OrganizationalProjectsQueryController : Controller
    {
        private readonly IMediator _mediator;

        public OrganizationalProjectsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        //[SimaAuthorize(Permissions.ServiceCategoryGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetOrganizationalProjectQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        //[SimaAuthorize(Permissions.ServiceCategoryGetAll)]
        public async Task<Result> Get([FromBody] GetAllOrganizationalProjectsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
