using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskCriterias
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskCriterias")]
    public class RiskCriteriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskCriteriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateRiskCriteriaCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyRiskCriteriaCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskCriteriaCommand { Id = id };
            return await _mediator.Send(command);
        }

    }
}
