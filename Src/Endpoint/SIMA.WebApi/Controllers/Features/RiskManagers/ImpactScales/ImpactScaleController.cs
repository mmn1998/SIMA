using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.ImpactScales;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ImpactScales
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ImpactScales")]
    public class ImpactScaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ImpactScaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateImpactScaleCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyImpactScaleCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteImpactScaleCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
