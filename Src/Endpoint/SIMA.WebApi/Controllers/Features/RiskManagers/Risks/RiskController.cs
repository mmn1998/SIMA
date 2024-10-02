using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.RiskManagers.Risks;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.Risks
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Risks")]
    public class RiskController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RiskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateRiskCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
