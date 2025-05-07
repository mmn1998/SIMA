using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ApiAuthenticationMethods
{
    [Route("basic/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiAuthenticationMethods")]
    [Authorize]
    public class ApiAuthenticationMethodsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiAuthenticationMethodsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.ApiAuthenticationMethodPost)]
        public async Task<Result> Post([FromBody] CreateApiAuthenticationMethodCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.ApiAuthenticationMethodPut)]
        public async Task<Result> Put([FromBody] ModifyApiAuthenticationMethodCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.ApiAuthenticationMethodDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteApiAuthenticationMethodCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
