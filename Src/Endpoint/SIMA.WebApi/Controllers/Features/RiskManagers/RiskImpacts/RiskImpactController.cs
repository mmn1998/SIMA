using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskImpacts;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskImpacts
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskImpacts")]
    public class RiskImpactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskImpactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskImpactPost)]
        public async Task<Result> Post([FromBody] CreateRiskImpactCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskImpactPut)]
        public async Task<Result> Put([FromBody] ModifyRiskImpactCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskImpactDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskImpactCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
