using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ServicePriorities;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServicePriorities
{
    [Route("serviceCatalog/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ServicePriorities")]
    [Authorize]
    public class ServicePrioritiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServicePrioritiesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.ServicePriorityPost)]
        public async Task<Result> Post([FromBody] CreateServicePriorityCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.ServicePriorityPut)]
        public async Task<Result> Put([FromBody] ModifyServicePriorityCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.ServicePriorityDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteServicePriorityCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
