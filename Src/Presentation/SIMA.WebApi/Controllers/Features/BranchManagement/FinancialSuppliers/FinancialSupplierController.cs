using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.FinancialSuppliers
{
    [Route("branch/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Branch/FinancialSuppliers")]
    [Authorize]
    public class FinancialSupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialSupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<Result> Post([FromBody] CreateFinancialSupplierCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        public async Task<Result> Put([FromBody] ModifyFinancialSupplierCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteFinancialSupplierCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
