using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.FinancialSuppliers
{
    [Route("branch/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Branch/FinancialSuppliers")]
    [Authorize]
    public class FinancialSupplierQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialSupplierQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("GetAll")]
        public async Task<Result> Get(GetAllFinancialSuppliersQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet("{id}")]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetFinancialSupplierQuery { Id = id };
            return await _mediator.Send(query);
        }
    }
}
