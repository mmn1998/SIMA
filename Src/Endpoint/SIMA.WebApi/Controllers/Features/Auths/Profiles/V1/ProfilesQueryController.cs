using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Profiles.V1;

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
    [SimaAuthorize(Permissions.ProfileGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetProfileQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.ProfileGetAll)]
    public async Task<Result> Get(GetAllProfileQuery request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpGet("shortProfile")]
    [SimaAuthorize(Permissions.ShortProfileGet)]
    public async Task<Result> GetShortProfile()
    {
        var query = new GetAllShortProfileQuery();
        var result = await _mediator.Send(query);
        return result;

    }

    [HttpPost("GetPhoneBooksByProfileId")]
    public async Task<Result> GetPhoneBookByProfileId(GetAllPhoneBookQuery request)
    {
        var result = await _mediator.Send(request);
        return result;
    }

    [HttpPost("GetAddressBooksByProfileId")]
    public async Task<Result> GetAddressBookByProfileId(GetAllAddressBookQuery request)
    {
        var result = await _mediator.Send(request);
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
