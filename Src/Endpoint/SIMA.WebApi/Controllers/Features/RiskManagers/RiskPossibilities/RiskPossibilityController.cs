using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskPossibilities;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskPossibilities
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskPossibilities")]
    public class RiskPossibilityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskPossibilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskPossibilitiesPost)]
        public async Task<Result> Post([FromBody] CreateRiskPossibilityCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskPossibilitiesPut)]
        public async Task<Result> Put([FromBody] ModifyRiskPossibilityCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskPossibilitiesDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskPossibilityCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
