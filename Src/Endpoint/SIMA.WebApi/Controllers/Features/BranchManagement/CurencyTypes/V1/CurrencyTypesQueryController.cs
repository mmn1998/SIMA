using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.BranchManagement.CurrencyTypes;
using SIMA.Framework.Common.Request;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.BranchManagement.CurencyTypes.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CurrencyTypes")]
[Authorize]
public class CurrencyTypesQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CurrencyTypesQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.CurrencyTypeGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetCurrencyTypeQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpGet]
    [SimaAuthorize(Permissions.CurrencyTypeGetAll)]
    public async Task<Result> Get([FromQuery] GetAllCurrencyTypesQuery query)
    {
        return await _mediator.Send(query);
    }
}
