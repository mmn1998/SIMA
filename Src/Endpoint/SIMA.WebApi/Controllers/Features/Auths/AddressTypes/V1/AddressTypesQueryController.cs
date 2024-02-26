using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        try
        {
            var query = new GetAddressTypeQuery { Id = id };
            var result = await _mediator.Send(query);
            return result;
        }
        catch (Exception e)
        {

            throw;
        }
    }

    [HttpGet]
    [SimaAuthorize(Permissions.AddressTypesGetAll)]
    public async Task<Result> Get([FromQuery] BaseRequest request)
    {
        var query = new GetAllAddressTypesQuery { Request = request };
        var result = await _mediator.Send(query);
        return result;
    }
}
