using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.FinancialActionTypes
{
    [Route("branch/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Branch/FinancialActionTypes")]
    [Authorize]
    public class FinancialActionTypeQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialActionTypeQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get(GetAllFinancialActionTypesQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetFinancialActionTypeQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
