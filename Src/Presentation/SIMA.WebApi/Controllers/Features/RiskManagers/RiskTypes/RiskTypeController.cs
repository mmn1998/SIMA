using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.RiskTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskTypes
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskTypes")]
    public class RiskTypeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.RiskTypesPost)]
        public async Task<Result> Post([FromBody] CreateRiskTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        [SimaAuthorize(Permissions.RiskTypesPut)]
        public async Task<Result> Put([FromBody] ModifyRiskTypeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.RiskTypesDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteRiskTypeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
