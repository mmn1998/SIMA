using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.BranchTypes.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Branch/BranchTypes")]
    [Authorize]
    public class BranchTypesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchTypesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.BranchTypeGetAll)]

        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetBranchTypeQuery { Id = id };
            return await _mediator.Send(query);
        }
        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.BranchTypeGet)]
        public async Task<Result> Get(GetAllBranchTypesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
