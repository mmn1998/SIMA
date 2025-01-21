using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.ServiceCatalog.ApiTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.ServiceCatalog.ApiTypes
{
    [Route("serviceCatalog/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiTypes")]
    [Authorize]
    public class ApiTypesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiTypesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.ApiTypeGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetApiTypeQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.ApiTypeGetAll)]
        public async Task<Result> Get([FromBody] GetAllApiTypesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
