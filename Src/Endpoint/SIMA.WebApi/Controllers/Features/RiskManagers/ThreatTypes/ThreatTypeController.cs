using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.ThreatTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.ThreatTypes
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ThreatTypes")]
    public class ThreatTypeController : Controller
    {
        private readonly IMediator _mediator;
        public ThreatTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateThreatTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyThreatTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteThreatTypeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
