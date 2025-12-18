using MediatR;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Logistics.CandidatedSuppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Logistics.CandidatedSuppliers.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "CandidatedSuppliers")]
public class CandidatedSuppliersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CandidatedSuppliersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.CandidatedSuppliersGetAll)]
    public async Task<Result> Get([FromBody] GetAllCandidatedSuppliersQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpPost("GetByLogesticSupplyId/{Id}")]
    [SimaAuthorize(Permissions.CandidatedSuppliersGetAll)]
    public async Task<Result> Get([FromRoute] long Id)
    {
        var result = new GetAllCandidatedSuppliersByLogesticIdQuery { Id = Id };
        return await _mediator.Send(result);
    }

    [HttpPost("GetSelectedSupplier/GetByLogesticSupplyId/{Id}")]
    [SimaAuthorize(Permissions.CandidatedSuppliersGetAll)]
    public async Task<Result> GetSelectedSupplier([FromRoute] long Id)
    {
        var result = new GetSelectedCandidatedSupplierQuery { LogisticsSupplyId = Id };
        return await _mediator.Send(result);
    }
}