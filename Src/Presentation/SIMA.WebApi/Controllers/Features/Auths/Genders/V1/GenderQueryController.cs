using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Gender;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Read.Repositories.Features.Auths.Genders;

namespace SIMA.WebApi.Controllers.Features.Auths.Genders.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Genders")]
[Authorize]

public class GendersQueryController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IGenderQueryRepository _genderQuery;

    public GendersQueryController(IMediator mediator, IGenderQueryRepository genderQuery)
    {
        _mediator = mediator;
        _genderQuery = genderQuery;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.GenderGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetGenderQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.GenderGetAll)]
    public async Task<Result> Get(GetAllGenderQuery request)
    {
        return await _mediator.Send(request);
        
    }
}
