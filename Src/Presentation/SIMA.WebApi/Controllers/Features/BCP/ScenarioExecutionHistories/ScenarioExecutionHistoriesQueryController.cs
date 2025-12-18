using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BCP.SenarioExecutionHistories;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.ScenarioExecutionHistories
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BCP/ScenarioExecutionHistories")]
    [Authorize]
    public class ScenarioExecutionHistoriesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScenarioExecutionHistoriesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.senarioExecutionHistoryGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetSenarioExecutionHistoryQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.senarioExecutionHistoryGetAll)]
        public async Task<Result> Get([FromBody] GetAllSenarioExecutionHistoriesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
