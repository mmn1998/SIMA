using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.Senarios;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.Scenarios
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BCP/Scenarios")]
    [Authorize]
    public class ScenariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ScenariosController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.senarioPost)]
        public async Task<Result> Post([FromBody] CreateSenarioCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.senarioPut)]
        public async Task<Result> Put([FromBody] ModifySenarioCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.senarioDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteScenarioCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
