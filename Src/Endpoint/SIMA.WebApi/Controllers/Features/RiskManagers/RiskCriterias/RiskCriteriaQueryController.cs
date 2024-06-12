using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.RiskManagers.RiskCriterias
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "RiskCriterias")]
    public class RiskCriteriaQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RiskCriteriaQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get([FromBody] GetAllRiskCriteriasQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetRiskCriteriaQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
