using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityPlans;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BCP.BusinessContinuityPlans
{
    [Route("bcp/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BCP/BusinessContinuityPlans")]
    [Authorize]
    public class BusinessContinuityPlansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessContinuityPlansController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.businessContinuityPlanPost)]
        public async Task<Result> Post([FromBody] CreateBusinessContinuityPlanCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.businessContinuityPlanPut)]
        public async Task<Result> Put([FromBody] ModifyBusinessContinuityPlanCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.businessContinuityPlanDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteBusinessContinuityPlanCommand { Id = id };
            return await _mediator.Send(command);
        }

        [HttpDelete("DeleteVersion/{id}/{BusinessContinuityPlanVersioningId}")]
        [SimaAuthorize(Permissions.businessContinuityPlanVersionDelete)]
        public async Task<Result> DeleteVersion([FromRoute] long id , long businessContinuityPlanVersioningId)
        {
            var command = new DeleteBusinessContinuityPlanVersioningCommand { Id = id , BusinessContinuityPlanVersioningId = businessContinuityPlanVersioningId };
            return await _mediator.Send(command);
        }
    }
}
