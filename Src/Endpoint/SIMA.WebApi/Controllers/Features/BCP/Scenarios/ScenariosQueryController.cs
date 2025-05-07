using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.Scenarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.Scenarios
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BCP/Scenarios")]
    [Authorize]
    public class ScenariosQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScenariosQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.senarioGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetScenarioQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.senarioGetAll)]
        public async Task<Result> Get([FromBody] GetAllScenariosQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
