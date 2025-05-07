using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.FinancialActionTypes
{
    [Route("branch/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Branch/FinancialActionTypes")]
    [Authorize]
    public class FinancialActionTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialActionTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateFinancialActionTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyFinancialActionTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteFinancialActionTypeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
