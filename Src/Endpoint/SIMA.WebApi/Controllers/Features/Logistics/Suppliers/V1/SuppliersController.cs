using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Logistics.Suppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.Suppliers.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Suppliers")]
[Authorize]
public class SuppliersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [SimaAuthorize(Permissions.SuppliersPost)]
    public async Task<Result> Post([FromBody] CreateSupplierCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.SuppliersPut)]
    public async Task<Result> Put([FromBody] ModifySupplierCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpDelete("{id}")]
    [SimaAuthorize(Permissions.SuppliersDelete)]
    public async Task<Result> Delete([FromRoute] long id)
    {
        var command = new DeleteSupplierCommand { Id = id };
        return await _mediator.Send(command);
    }
}