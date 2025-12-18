using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ServiceStatuses;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ServiceStatuses
{
    [Route("serviceCatalog/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ServiceStatus")]
    [Authorize]
    public class ServiceStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.ServiceStatusPost)]
        public async Task<Result> Post([FromBody] CreateServiceStatusCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.ServiceStatusPut)]
        public async Task<Result> Put([FromBody] ModifyServiceStatusCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.ServiceStatusDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteServiceStatusCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
