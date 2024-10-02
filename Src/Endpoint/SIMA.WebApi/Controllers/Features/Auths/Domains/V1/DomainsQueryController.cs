using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Domains;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Domains.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Domains")]
[Authorize]

public class DomainsQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DomainsQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetDomainQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.DomainsGetAll)]
    public async Task<Result> Get(GetAllDomainQuery request)
    {
        var result = await _mediator.Send(request);
        return result;
    }
}
