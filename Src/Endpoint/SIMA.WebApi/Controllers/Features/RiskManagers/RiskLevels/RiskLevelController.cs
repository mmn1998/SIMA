using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevels;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevels
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskLevels")]
    public class RiskLevelController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskLevelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskLevelPost)]
        public async Task<Result> Post([FromBody] CreateRiskLevelCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskLevelPut)]
        public async Task<Result> Put([FromBody] ModifyRiskLevelCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskLevelDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskLevelCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
