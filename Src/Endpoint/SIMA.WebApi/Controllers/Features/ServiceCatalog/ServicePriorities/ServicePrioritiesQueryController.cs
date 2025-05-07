using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServicePriorities
{
    [Route("serviceCatalog/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ServicePriorities")]
    [Authorize]
    public class ServicePrioritiesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicePrioritiesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.ServicePriorityGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetServicePriorityQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.ServicePriorityGetAll)]
        public async Task<Result> Get([FromBody] GetAllServicePrioritiesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
