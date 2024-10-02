using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiAuthenticationMethods;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ApiAuthenticationMethods
{
    [Route("basic/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiAuthenticationMethods")]
    [Authorize]
    public class ApiAuthenticationMethodsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiAuthenticationMethodsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetApiAuthenticationMethodQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get([FromBody] GetAllApiAuthenticationMethodsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
