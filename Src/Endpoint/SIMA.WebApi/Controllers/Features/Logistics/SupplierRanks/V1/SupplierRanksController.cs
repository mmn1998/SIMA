using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.SupplierRanks;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Logistics.SupplierRanks.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "SupplierRanks")]
public class SupplierRanksController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierRanksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<Result> Post([FromBody] CreateSupplierRankCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    public async Task<Result> Put([FromBody] ModifySupplierRankCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSupplierRankCommand { Id = id };
        return await _mediator.Send(command);
    }
}