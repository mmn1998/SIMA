using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Companies;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Companies.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Companies")]
    [Authorize]

    public class CompaniesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompaniesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.CompanyGet)]
        public async Task<Result> Get(long id)
        {
            var query = new GetCompanyByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        //    [SimaAuthorize(Permissions.CompanyGetAll)]
        public async Task<Result> Get([FromQuery] BaseRequest request)
        {
            var query = new GetAllCompanyQuery { Request = request };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
