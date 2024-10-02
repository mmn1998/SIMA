using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Query.Contract.Features.Auths.Suppliers;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.Auths.Suppliers.V1;

[Route("[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = "Suppliers")]
[Authorize]
public class SuppliersQueryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersQueryController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("{id}")]
    [SimaAuthorize(Permissions.SuppliersGet)]
    public async Task<Result> Get([FromRoute] long id)
    {
        var query = new GetSupplierQuery { Id = id };
        return await _mediator.Send(query);
    }
    [HttpPost("GetAllOrderedNotInBlack")]
    [SimaAuthorize(Permissions.SuppliersGetAll)]
    public async Task<Result> Get([FromBody] GetAllOrderedNotInBlackListSuppliersQuery query)
    {
        return await _mediator.Send(query);
    }
    [HttpPost("GetAll")]
    [SimaAuthorize(Permissions.SuppliersGetAll)]
    public async Task<Result> Get([FromBody] GetAllSuppliersQuery query)
    {
        return await _mediator.Send(query);
    }
}