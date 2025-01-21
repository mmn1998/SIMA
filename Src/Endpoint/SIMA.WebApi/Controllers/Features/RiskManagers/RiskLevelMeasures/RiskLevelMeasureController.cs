using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskLevelMeasures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevelMeasures
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskLevelMeasures")]
    public class RiskLevelMeasureController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskLevelMeasureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskLevelMeasuresPost)]
        public async Task<Result> Post([FromBody] CreateRiskLevelMeasureCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskLevelMeasuresPut)]
        public async Task<Result> Put([FromBody] ModifyRiskLevelMeasureCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskLevelMeasuresDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskLevelMeasureCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
