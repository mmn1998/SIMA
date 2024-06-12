using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Departments.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Departments")]
    [Authorize]

    public class DepartmentsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.DepartmentsPut)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetDepartmentQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.DepartmentsPut)]
        public async Task<Result> Get(GetAllDepartmentsQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}
