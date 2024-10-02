using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Positions;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Positions.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Positions")]
    [Authorize]

    public class PositionsQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PositionsQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.PositionsGet)]
        public async Task<Result> Get([FromRoute] long id)
        {
            var query = new GetPositionQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("GetAll")]
        [SimaAuthorize(Permissions.PositionsGetAll)]
        public async Task<Result> Get(GetAllPositionsQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet("GetByDepartment/{DepartmentId}")]
        [SimaAuthorize(Permissions.PositionsGetAll)]
        public async Task<Result> GetByDepartment([FromRoute] long DepartmentId)
        {
            var query = new GetPositionByDepartemantQuery { DepartmentId = DepartmentId };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
