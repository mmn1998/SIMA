using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.SupplierRanks;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.SupplierRanks.V1;

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
    [SimaAuthorize(Permissions.SupplierRanksPost)]
    public async Task<Result> Post([FromBody] CreateSupplierRankCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.SupplierRanksPut)]
    public async Task<Result> Put([FromBody] ModifySupplierRankCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.SupplierRanksDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSupplierRankCommand { Id = id };
        return await _mediator.Send(command);
    }
}