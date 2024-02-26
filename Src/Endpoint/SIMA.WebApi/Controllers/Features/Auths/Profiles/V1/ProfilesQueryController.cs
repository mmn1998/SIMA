using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Profiles.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Profiles")]
    [Authorize]

    public class ProfilesQueryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesQueryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        [SimaAuthorize(Permissions.ProfileGetAll)]
        public async Task<Result> Get(long id)
        {
            var query = new GetProfileQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet]
        [SimaAuthorize(Permissions.ProfileGet)]
        public async Task<Result> Get([FromQuery] BaseRequest request)
        {
            var query = new GetAllProfileQuery { Request = request };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("shortProfile")]
        [SimaAuthorize(Permissions.UserGroupPut)]
        public async Task<Result> GetShortProfile()
        {
            var query = new GetAllShortProfileQuery();
            var result = await _mediator.Send(query);
            return result;

        }

        [HttpGet("GetPhoneBooksByProfileId/{ProfileId}")]
        public async Task<Result> GetPhoneBookByProfileId(int ProfileId, [FromQuery] BaseRequest baseRequest)
        {
            var query = new GetAllPhoneBookQuery { Id = ProfileId, Request = baseRequest };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetAddressBooksByProfileId/{ProfileId}")]
        public async Task<Result> GetAddressBookByProfileId(int ProfileId, [FromQuery] BaseRequest baseRequest)
        {
            var query = new GetAllAddressBookQuery { Id = ProfileId };
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetManagersByCompanyId/{companyId}")]
        public async Task<Result<List<SelectModel>>> GetManagerByCompanyId(int companyId)
        {
            var query = new GetManagersByCompanyId { Id = companyId };
            var result = await _mediator.Send(query);
            return result;
        }
    }
}
