using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.ResponsibleTypes.V1
{
    [Route("basic/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ResponsibleTypes")]
    [Authorize]

    public class ResponsibleTypeQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResponsibleTypeQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        // [SimaAuthorize(Permissions.ResponsibleTypeGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetResponsibleTypeQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpPost("GetAll")]
        //    [SimaAuthorize(Permissions.ResponsibleTypeGetAll)]
        public async Task<Result> Get(GetAllResponsibleTypeQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}
