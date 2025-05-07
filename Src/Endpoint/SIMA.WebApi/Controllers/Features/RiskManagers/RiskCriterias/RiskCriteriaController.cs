using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskCriterias;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

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
        [SimaAuthorize(Permissions.RiskCriteriasPost)]
        public async Task<Result> Post([FromBody] CreateRiskCriteriaCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskCriteriasPut)]
        public async Task<Result> Put([FromBody] ModifyRiskCriteriaCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskCriteriasDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskCriteriaCommand { Id = id };
            return await _mediator.Send(command);
        }

    }
}
