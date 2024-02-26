using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.BrokerTypes.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BrokerTypes")]
    [Authorize]
    public class BrokerTypesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrokerTypesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.BrokerTypeGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetBrokerTypeQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpGet]
        [SimaAuthorize(Permissions.BrokerTypeGetAll)]
        public async Task<Result> Get([FromQuery] BaseRequest request)
        {
            var query = new GetAllBrokerTypesQuery { Request = request };
            return await _mediator.Send(query);
        }
    }
}
