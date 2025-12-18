using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskLevelMeasures
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskLevelMeasures")]
    public class RiskLevelMeasureQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RiskLevelMeasureQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.RiskLevelMeasuresGetAll)]
        public async Task<Result> Get([FromBody] GetAllRiskLevelMeasuresQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.RiskLevelMeasuresGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetRiskLevelMeasureQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
