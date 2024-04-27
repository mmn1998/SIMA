using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowCompany;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.WorkFlowCompany.V1
{

    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WorkflowCompany")]
    [Authorize]
    public class WorkflowCompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkflowCompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SimaAuthorize(Permissions.WorkFlowCompanyPost)]
        public async Task<Result> Post([FromBody] CreateWorkFlowCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut]
        [SimaAuthorize(Permissions.WorkFlowCompanyPut)]
        public async Task<Result> Put([FromBody] ModifyWorkFlowCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.WorkFlowCompanyDelete)]
        public async Task<Result> Delete(long id)
        {
            var command = new DeleteWorkFlowCompanyCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
