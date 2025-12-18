using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.SupplierRanks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.SupplierRanks.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SupplierRanks")]
[Authorize]
public class SupplierRanksQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierRanksQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.SupplierRanksGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSupplierRankQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.SupplierRanksGetAll)]
    public async Task<Result> Get([FromBody] GetAllSupplierRanksQuery query)
    {
        return await _mediator.Send(query);
    }
}