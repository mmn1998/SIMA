using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.AddressTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.AddressTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "AddressTypes")]

public class AddressTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.AddressTypesGet)]
    public async Task<Result> Get(long id)
    {
        var query = new GetAddressTypeQuery { Id = id };
        var result = await _mediator.Send(query);
        return result;
    }

    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.AddressTypesGetAll)]
    public async Task<Result> Get(GetAllAddressTypesQuery request)
    {
        return await _mediator.Send(request);
    }
}
