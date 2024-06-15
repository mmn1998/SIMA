using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.SupplierRanks;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.SupplierRanks.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SupplierRanks")]
public class SupplierRanksQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierRanksQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSupplierRankQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    public async Task<Result> Get([FromBody] GetAllSupplierRanksQuery query)
    {
        return await _mediator.Send(query);
    }
}