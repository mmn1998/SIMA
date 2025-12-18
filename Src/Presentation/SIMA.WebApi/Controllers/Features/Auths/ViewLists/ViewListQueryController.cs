using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists;
using SIMA.Application.Query.Contract.Features.Auths.ViewLists.ViewField;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.ViewLists
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ViewLists")]
    public class ViewListQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewListQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetAll")]
        public async Task<Result> Get(GetAllViewListQuery request)
        {
            request = new GetAllViewListQuery();
            return await _mediator.Send(request);
        }

        [HttpPost("GetViewFeild")]

        public async Task<Result> Get(GetAllViewFieldQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}
