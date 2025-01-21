using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskDegrees;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskDegrees
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskDegrees")]
    public class RiskDegreeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskDegreeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskDegreesPost)]
        public async Task<Result> Post([FromBody] CreateRiskDegreeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskDegreesPut)]
        public async Task<Result> Put([FromBody] ModifyRiskDegreeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskDegreesDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskDegreeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
