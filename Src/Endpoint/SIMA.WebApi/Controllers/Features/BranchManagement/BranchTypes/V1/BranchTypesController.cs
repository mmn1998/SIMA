using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.BranchTypes.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "BranchTypes")]
    [Authorize]
    public class BranchTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BranchTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [SimaAuthorize(Permissions.BranchTypePost)]
        public async Task<Result> Post([FromBody] CreateBranchTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [SimaAuthorize(Permissions.BranchTypePut)]
        public async Task<Result> Put([FromBody] ModifyBranchTypeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{id}")]
        [SimaAuthorize(Permissions.BranchTypeDelete)]
        public async Task<Result> Delete([FromRoute] long id)
        {
            var command = new DeleteBranchTypeCommand { Id = id };
            return await _mediator.Send(command);
        }
    }
}
